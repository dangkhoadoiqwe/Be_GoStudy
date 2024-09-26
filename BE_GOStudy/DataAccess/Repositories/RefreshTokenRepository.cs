using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NTQ.Sdk.Core.BaseConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public partial interface IRefreshTokenRepository : IBaseRepository<RefreshToken>
    {
        Task<string?> GetGoogleAccessTokenByUserIdAsync(int userId);
        Task<string?> RefreshAccessTokenAsync(string refreshToken);
        Task<string?> GetRefreshTokenByUserIdAsync(int userId); // Thêm phương thức này
    }

    public partial class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
    {
        private readonly DbContext _dbContext;
        private readonly IConfiguration _configuration;

        public RefreshTokenRepository(DbContext dbContext, IConfiguration configuration) : base(dbContext)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<string?> RefreshAccessTokenAsync(string refreshToken)
        {
            var client = new HttpClient();
            var values = new Dictionary<string, string>
            {
                { "client_id", _configuration["Google:ClientId"] },
                { "client_secret", _configuration["Google:ClientSecret"] },
                { "refresh_token", refreshToken },
                { "grant_type", "refresh_token" }
            };

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("https://oauth2.googleapis.com/token", content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                dynamic tokenResponse = JsonConvert.DeserializeObject(responseString);
                return tokenResponse.access_token; // Trả về Access Token mới
            }

            return null; // Hoặc xử lý lỗi
        }

        public async Task<string?> GetGoogleAccessTokenByUserIdAsync(int userId)
        {
            // Lấy refresh token mới nhất của người dùng
            var refreshToken = await _dbContext.Set<RefreshToken>()
                .Where(rt => rt.UserId == userId && !rt.IsRevoked && !rt.IsUsed)
                .OrderByDescending(rt => rt.IssuedAt)  // Sắp xếp theo thời gian phát hành để lấy token mới nhất
                .FirstOrDefaultAsync();

            // Kiểm tra xem token có tồn tại và có hợp lệ không
            if (refreshToken == null || refreshToken.ExpriedAt < DateTime.UtcNow)
            {
                return null;  // Token không tồn tại hoặc đã hết hạn
            }

            // Trả về Google Access Token
            return refreshToken.AccessTokenGoogle;
        }

        public async Task<string?> GetRefreshTokenByUserIdAsync(int userId)
        {
            // Lấy refresh token mới nhất của người dùng
            var refreshToken = await _dbContext.Set<RefreshToken>()
                .Where(rt => rt.UserId == userId && !rt.IsRevoked && !rt.IsUsed)
                .OrderByDescending(rt => rt.IssuedAt)  // Sắp xếp theo thời gian phát hành để lấy token mới nhất
                .FirstOrDefaultAsync();

            // Kiểm tra xem token có tồn tại không
            if (refreshToken == null || refreshToken.ExpriedAt < DateTime.UtcNow)
            {
                return null;  // Token không tồn tại hoặc đã hết hạn
            }

            // Trả về Refresh Token
            return refreshToken.Token;
        }
    }
}
