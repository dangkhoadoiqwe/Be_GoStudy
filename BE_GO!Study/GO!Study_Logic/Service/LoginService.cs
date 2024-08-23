
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

namespace GO_Study_Logic.Service
{
    public class LoginService
    {
        private readonly IUserRepository _userRepository;

        private readonly IConfiguration _configuration;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IMapper _mapper;

        public LoginService(IUserRepository userRepository, IConfiguration configuration
            , TokenValidationParameters tokenValidationParameters, IRefreshTokenRepository refreshTokenRepository, IMapper mapper)
        {

            _configuration = configuration;
            _tokenValidationParameters = tokenValidationParameters;
            _refreshTokenRepository = refreshTokenRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<LoginModel> login(LoginRequest login)
        {
            var userExist = _userRepository.FirstOrDefault(u => u.Email == login.Email);
            var userModel = _mapper.Map<UserViewModel>(userExist);
            string storedPassword = userExist.PasswordHash;
            string passwordToCheck = login.Password;

            bool passwordVerified = false;
            if (userExist is null)
            {
                throw new ErrorResponse(404, 4, "User not exist");

            }
            if (storedPassword.Length == 60 && storedPassword.StartsWith("$2a$"))
            {
                // Mật khẩu đã được mã hóa, sử dụng Verify của BCrypt để xác minh
                passwordVerified = BCrypt.Net.BCrypt.Verify(passwordToCheck, storedPassword);
            }
            else
            {
                // Mật khẩu chưa được mã hóa, mã hóa mật khẩu trước khi xác minh
                passwordVerified = (passwordToCheck == storedPassword);
            }

            if (!passwordVerified)
            {
                throw new ErrorResponse(404, 4, "Wrong Password");
            }

            return new LoginModel
            {
                Success = true,
                UserName = userModel.Email,
                Role = Enum.GetName(typeof(RoleEnum), userModel.Role),
                Data = GenerateJwtToken(userModel),

            };

        }

        public TokenModel GenerateJwtToken(UserViewModel userModel)
        {
            var user = _mapper.Map<User>(userModel);
            var expries = DateTime.UtcNow.Add(TimeSpan.Parse(_configuration.GetSection("JwtSettings:ExpiryTimeFrame").Value));
            var handler = new JwtSecurityTokenHandler();


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings:Key").Value));
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sid,user.UserId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub,user.FullName),
                    new Claim(JwtRegisteredClaimNames.Email,user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat,DateTime.Now.ToUniversalTime().ToString()),
                    new Claim(ClaimTypes.Role, Enum.GetName(typeof(RoleEnum), user.role))
        }),
                Expires = expries,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)

            };
            var token = handler.CreateToken(tokenDescription);
            var accessToken = handler.WriteToken(token);
            var refreshToken = new RefreshToken
            {
                RefreshTokenId = Guid.NewGuid(),
                JwtId = token.Id,
                Token = RandomStringGeneration(),
                IssuedAt = DateTime.UtcNow,
                ExpriedAt = DateTime.UtcNow.AddMonths(1),
                IsRevoked = false,
                IsUsed = false,
                UserId = user.UserId

            };
            _refreshTokenRepository.Create(refreshToken);
            _refreshTokenRepository.Save();
            return new TokenModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };
        }
        public async Task<UserModel> VerifyAndGenerateToken(TokenModel token)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            try
            {
                //_tokenValidationParameters.ValidateLifetime = false;
                var tokenVerification = jwtTokenHandler.ValidateToken(token.AccessToken, _tokenValidationParameters, out var validatedToken);
                if (validatedToken is JwtSecurityToken jwtToken)
                {
                    var result = jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
                    if (result == false)

                        return null;

                }
                var utcExpiryDate = long.Parse(tokenVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);
                if (expiryDate > DateTime.Now)
                {
                    return new UserModel
                    {
                        Success = false,
                        Message = "Expried Token"
                    };
                }
                var storedToken = await _refreshTokenRepository.FirstOrDefaultAsync(x => x.Token == token.RefreshToken);
                if (storedToken == null)
                {
                    return new UserModel
                    {
                        Success = false,
                        Message = "Refresh token does not exist"
                    };
                }
                if (storedToken.IsUsed)
                {
                    return new UserModel
                    {
                        Success = false,
                        Message = "Refresh token has been used"
                    };
                }
                if (storedToken.IsRevoked)
                {
                    return new UserModel
                    {
                        Success = false,
                        Message = "Refresh token has been revoked"
                    };
                }
                var jti = tokenVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                if (storedToken.JwtId != jti)
                {
                    return new UserModel
                    {
                        Success = false,
                        Message = "Invalid Token"
                    };
                }
                if (storedToken.ExpriedAt < DateTime.UtcNow)
                {
                    return new UserModel
                    {
                        Success = false,
                        Message = "Exprired Token"
                    };
                }
                storedToken.IsUsed = true;
                _refreshTokenRepository.Update(storedToken);
                await _refreshTokenRepository.SaveAsync();
                var dbUser = await _userRepository.FirstOrDefaultAsync(u => u.UserId == storedToken.UserId);
                var userModel = _mapper.Map<UserViewModel>(dbUser);
                var accessToken = GenerateJwtToken(userModel);
                return new UserModel
                {
                    Success = true,
                    Data = accessToken,
                    Message = "Verify And GenerateToken success"
                };
            }
            catch (Exception ex)
            {
                return new UserModel
                {
                    Success = false,
                    Message = ex.Message,
                };
            }

        }

        private DateTime UnixTimeStampToDateTime(long utcExpiryDate)
        {
            var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(utcExpiryDate).ToUniversalTime();
            return dateTimeVal;
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
