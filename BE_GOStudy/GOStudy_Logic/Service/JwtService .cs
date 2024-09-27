using DataAccess.Model;
using DataAccess.Repositories;
using GO_Study_Logic.ViewModel;
using GO_Study_Logic.ViewModel.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
namespace GO_Study_Logic.Service
{
    public interface IJwtService
    {
        TokenModel GenerateJwtToken(User user);
        string GenerateJwtToke(AppUser user);
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
                UserId = userId,
             //   AccessTokenGoogle = accessTokenGoogle  // Lưu Access Token từ Google
            };

            _refreshTokenRepository.Create(refreshToken);
            _refreshTokenRepository.Save();
            return refreshToken;
        }


        public string GenerateJwtToke(AppUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }

            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.FullName))
            {
                throw new ArgumentException("User email or full name cannot be null or empty.");
            }

            // Lấy thời gian hết hạn của token
            var expires = DateTime.UtcNow.Add(TimeSpan.Parse(_configuration["JwtSettings:ExpiryTimeFrame"]));

            // Tạo security key từ cấu hình
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // Payload token theo format yêu cầu
            var tokenPayload = new Dictionary<string, object>
    {
        { "context", new Dictionary<string, object>
            {
                { "user", new Dictionary<string, object>
                    {
                        { "avatar", user.ProfileImage },  // Avatar của người dùng
                        { "name", user.FullName },        // Tên của người dùng
                        { "email", user.Email },          // Email của người dùng
                        { "id", user.UserId.ToString() }  // ID người dùng
                    }
                },
                { "group", "default-group" }  // Thêm group nếu cần
            }
        },
        { "aud", "meet-jit-si-66cbd" },          // Audience
        { "iss", "https://securetoken.google.com/meet-jit-si-66cbd" }, // Issuer
        { "sub", user.UserId.ToString() },       // Subject (user ID)
        { "room", "*" },                         // Phòng họp hoặc thông tin khác
        { "exp", new DateTimeOffset(expires).ToUnixTimeSeconds() } // Thời gian hết hạn
    };

            // Chuyển đổi Dictionary thành danh sách Claim
            var claims = tokenPayload.Select(kv => new Claim(kv.Key, JsonConvert.SerializeObject(kv.Value))).ToList();

            // Tạo token với JWT Security Token Handler
            var handler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = expires,
                SigningCredentials = creds,
                Subject = new ClaimsIdentity(claims)  // Đưa toàn bộ payload vào claims dưới dạng Claim
            };

            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }
       
        public TokenModel GenerateJwtToken(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }

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
            new Claim(JwtRegisteredClaimNames.Sid, user.UserId.ToString()),  // User ID
            new Claim(ClaimTypes.Role, Enum.GetName(typeof(RoleEnum), user.Role)),  // sub, same as User ID
            new Claim(JwtRegisteredClaimNames.Email, user.Email),           // Email
            new Claim(JwtRegisteredClaimNames.Iss, "https://securetoken.google.com/meet-jit-si-66cbd"), // Issuer
            new Claim(JwtRegisteredClaimNames.Aud, "meet-jit-si-66cbd"),    // Audience
            new Claim("picture", user.ProfileImage),                        // Profile image
            new Claim("email_verified", true.ToString().ToLower()),         // Email verified status
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Token ID (JTI)
        }),
                Expires = expires,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = handler.CreateToken(tokenDescription);
            var accessToken = handler.WriteToken(token);

            // Lưu Google Access Token cùng với refresh token
            var refreshToken = CreateRefreshToken(token.Id, user.UserId);

            return new TokenModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                 // Trả lại Google Access Token trong model nếu cần sử dụng sau
            };
        }

    }
}
