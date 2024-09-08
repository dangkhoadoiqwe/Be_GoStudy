using DataAccess.Model;
using DataAccess.Repositories;
using GO_Study_Logic.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GO_Study_Logic.Service
{
    public interface IJwtService
    {
        TokenModel GenerateJwtToken(User user);
    }

    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public JwtService(IConfiguration configuration, IRefreshTokenRepository refreshTokenRepository)
        {
            _configuration = configuration;
            _refreshTokenRepository = refreshTokenRepository;
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

        public TokenModel GenerateJwtToken(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }

            // Ensure user properties are not null
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.FullName))
            {
                throw new ArgumentException("User email or full name cannot be null or empty.");
            }

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
                RefreshToken = refreshToken.Token,
                userID = user.UserId, 
            };
        }
    }
}