
using Microsoft.EntityFrameworkCore; 
using System.Linq.Expressions;
using System.Xml.Linq;
using DataAccess.Model;
using NTQ.Sdk.Core.BaseConnect;
namespace DataAccess.Repositories
{


    public partial interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByIdAsync(int id);
        Task<IEnumerable<Ranking>> GetAllRankingAsync();
        Task<IEnumerable<Attendance>> GetUserIdAtendanceAsync(int id);
        Task<IEnumerable<Tasks>> GetAllTaskByUserIDAsync(int userId);
        Task<IEnumerable<FriendRequest>> GetAllFriendRequestsAsync(int userid);
        Task<PrivacySetting?> GetPrivacySettingByuserIDAsync(int userid);
        Task<BlogPost?> Get1BlogPostAsync(int id);
        Task<int> TotalAttendance(int userid);
       
        Task<IEnumerable<Analytic>> GetUserIdAnalyticAsync(int userid);
    }
    public partial class UserRepository : BaseRepository<User>, IUserRepository
    
    {
        private readonly DbContext _dbContext;
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BlogPost?> Get1BlogPostAsync(int id)
        {
            return await _dbContext.Set<BlogPost>().FirstOrDefaultAsync(Bl =>Bl.UserId == id);
        }
        public async Task<User?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<User>().FindAsync(id);
        }
        

        public async Task<IEnumerable<Ranking>> GetAllRankingAsync()
        {
            return await _dbContext.Set<Ranking>().Take(10).ToListAsync();
        }

        public async Task<IEnumerable<Tasks>> GetAllTaskByUserIDAsync(int userId)
        {
            return await _dbContext.Set<Tasks>().Where(t => t.UserId == userId ).ToListAsync();
        }

        public async Task<IEnumerable<Attendance>> GetUserIdAtendanceAsync(int id)
        {
            return await _dbContext.Set<Attendance>().Where(Attendance => Attendance.UserId == id).ToListAsync();
        }
        public async Task<IEnumerable<FriendRequest>> GetAllFriendRequestsAsync(int userid)
        {
            return await _dbContext.Set<FriendRequest>()
                .Where(Fr => Fr.RecipientId == userid) 
                .ToListAsync();
        }

        public async Task<PrivacySetting?> GetPrivacySettingByuserIDAsync(int userid)
        {
            var user = await _dbContext.Set<User>().Include(u=>u.PrivacySetting).FirstOrDefaultAsync( u=>u.UserId == userid);
            return user?.PrivacySetting;
        }

        public async Task<IEnumerable<Analytic>> GetUserIdAnalyticAsync(int userid)
        {
            return await _dbContext.Set<Analytic>().Where(t => t.UserId == userid).ToListAsync();
        }

        public async Task<int> TotalAttendance(int userid)
        {
            return await _dbContext.Set<Attendance>()
                .CountAsync(a => a.UserId == userid);
        }

        
    }
}


