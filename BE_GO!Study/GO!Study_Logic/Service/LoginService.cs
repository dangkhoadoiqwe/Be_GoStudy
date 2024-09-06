
using AutoMapper;
using DataAccess.Model;
using DataAccess.Repositories;
using GO_Study_Logic.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using NTQ.Sdk.Core.Filters;
using GO_Study_Logic.ViewModel.User;

namespace GO_Study_Logic.Service
{
    public class LoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IMapper _mapper;

        public LoginService(IUserRepository userRepository, IConfiguration configuration,
                            TokenValidationParameters tokenValidationParameters,
                            IRefreshTokenRepository refreshTokenRepository,
                            IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _tokenValidationParameters = tokenValidationParameters;
            _refreshTokenRepository = refreshTokenRepository;
            _mapper = mapper;
        }

        public async Task<LoginModel> Login(LoginRequest login)
        {
            var userExist = await _userRepository.FirstOrDefaultAsync(u => u.Email == login.Email);
            if (userExist == null)
            {
                throw new ErrorResponse(404, 4, "User does not exist");
            }

            bool passwordVerified = VerifyPassword(login.Password, userExist.PasswordHash);
            if (!passwordVerified)
            {
                throw new ErrorResponse(404, 4, "Wrong Password");
            }

            var userModel = _mapper.Map<UserViewModel1>(userExist);
            return new LoginModel
            {
                Success = true,
                UserName = userModel.Email,
                Role = Enum.GetName(typeof(RoleEnum), userModel.Role),
                Data = GenerateJwtToken(userModel),
                userId = userModel.UserId,
            };
        }

        private bool VerifyPassword(string inputPassword, string storedPassword)
        {
            return storedPassword.Length == 60 && storedPassword.StartsWith("$2a$")
                ? BCrypt.Net.BCrypt.Verify(inputPassword, storedPassword)
                : inputPassword == storedPassword;
        }

        public TokenModel GenerateJwtToken(UserViewModel1 userModel)
        {
            var user = _mapper.Map<User>(userModel);
            var expires = DateTime.UtcNow.Add(TimeSpan.Parse(_configuration["JwtSettings:ExpiryTimeFrame"]));
            var handler = new JwtSecurityTokenHandler();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(JwtRegisteredClaimNames.Sid, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.FullName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
                Expires = expires,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = handler.CreateToken(tokenDescription);
            var accessToken = handler.WriteToken(token);
            var refreshToken = CreateRefreshToken(token.Id, user.UserId);

            return new TokenModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };
        }

        private RefreshToken CreateRefreshToken(string jwtId, int userId)
        {
            var refreshToken = new RefreshToken
            {
                RefreshTokenId = Guid.NewGuid(),
                JwtId = jwtId,
                Token = RandomStringGeneration(),
                IssuedAt = DateTime.UtcNow,
                ExpriedAt = DateTime.UtcNow.AddMonths(1),
                IsRevoked = false,
                IsUsed = false,
                UserId = userId
            };

            _refreshTokenRepository.Create(refreshToken);
            _refreshTokenRepository.Save();
            return refreshToken;
        }

        public async Task<UserModel> VerifyAndGenerateToken(TokenModel token)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var tokenVerification = jwtTokenHandler.ValidateToken(token.AccessToken, _tokenValidationParameters, out var validatedToken);
                if (validatedToken is JwtSecurityToken jwtToken && !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return new UserModel { Success = false, Message = "Invalid Token" };
                }

                var expiryDate = UnixTimeStampToDateTime(long.Parse(tokenVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp)?.Value ?? "0"));
                if (expiryDate < DateTime.UtcNow)
                {
                    return new UserModel { Success = false, Message = "Expired Token" };
                }

                var storedToken = await _refreshTokenRepository.FirstOrDefaultAsync(x => x.Token == token.RefreshToken);
                if (storedToken == null || storedToken.IsUsed || storedToken.IsRevoked)
                {
                    return new UserModel { Success = false, Message = "Invalid or used/revoked Refresh token" };
                }

                if (storedToken.JwtId != tokenVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)?.Value)
                {
                    return new UserModel { Success = false, Message = "Invalid Token" };
                }

                storedToken.IsUsed = true;
                _refreshTokenRepository.Update(storedToken);
                await _refreshTokenRepository.SaveAsync();

                var dbUser = await _userRepository.FirstOrDefaultAsync(u => u.UserId == storedToken.UserId);
                var userModel = _mapper.Map<UserViewModel1>(dbUser);
                var accessToken = GenerateJwtToken(userModel);

                return new UserModel { Success = true, Data = accessToken, Message = "Verify And Generate Token success" };
            }
            catch (Exception ex)
            {
                return new UserModel { Success = false, Message = ex.Message };
            }
        }

        private DateTime UnixTimeStampToDateTime(long utcExpiryDate)
        {
            return DateTimeOffset.FromUnixTimeSeconds(utcExpiryDate).UtcDateTime;
        }

        private string RandomStringGeneration()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }
    }
}
