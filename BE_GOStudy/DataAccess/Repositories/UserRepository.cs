
using Microsoft.EntityFrameworkCore; 
using System.Linq.Expressions;
using System.Xml.Linq;
using DataAccess.Model;

using NTQ.Sdk.Core.BaseConnect;
using System.Linq.Dynamic.Core.Tokenizer;
using static DataAccess.Repositories.UserRepository;
namespace DataAccess.Repositories
{


    public partial interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetUserByEmailAsync(string email);
        Task CreateUserAsync(User user);
        Task<User?> GetByIdAsync(int id);
        Task<IEnumerable<Ranking>> GetAllRankingAsync();
        Task<IEnumerable<Attendance>> GetUserIdAtendanceAsync(int id);
        Task<IEnumerable<Tasks>> GetAllTaskByUserIDAsync(int userId);
        Task<IEnumerable<FriendRequest>> GetAllFriendRequestsAsync(int userid);
        Task<PrivacySetting?> GetPrivacySettingByuserIDAsync(int userid);
        Task<BlogPost?> Get1BlogPostAsync(int id);
        Task<int> TotalAttendance(int userid);
        Task UpdateUserAsync(User user);
        Task<bool> CheckToken(int userid);
        Task<IEnumerable<Analytic>> GetUserIdAnalyticAsync(int userid);
        Task<IEnumerable<SpecializationUserDetailViewModel>> GetSpecializationDetailsByUserIdAsync(int userId);
        Task<IEnumerable<FriendRequest>> GetAllFriendRecipientsAsync(int userId);

        Task<IEnumerable<FriendRequest>> GetFriendsListAsync(int userId);

        Task<IEnumerable<User>> getAlluser();
    }
    public partial class UserRepository : BaseRepository<User>, IUserRepository
    
    {
        private readonly DbContext _dbContext;
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BlogPost?> Get1BlogPostAsync(int userId)
        {
            return await _dbContext.Set<BlogPost>()
                .Where(bl => bl.UserId == userId) // Lọc theo UserId
                .OrderByDescending(bl => bl.CreatedAt) // Sắp xếp theo CreateAt giảm dần
                .FirstOrDefaultAsync(); // Lấy bài đăng đầu tiên
        }


        public async Task<User?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<User>().FindAsync(id);
        }
       
        public async Task<IEnumerable<SpecializationUserDetailViewModel>> GetSpecializationDetailsByUserIdAsync(int userId)
        {
            var currentDate = DateTime.Now;

            var specializations = await _dbContext.Set<UserSpecialization>()
                .Where(us => us.UserId == userId)
                .Join(
                    _dbContext.Set<Specialization>(),
                    us => us.SpecializationId,
                    s => s.SpecializationId,
                    (us, s) => new SpecializationUserDetailViewModel
                    {
                        SpecializationName = s.Name,
                        Status = (currentDate - us.DateStart).Days > 60 // true if > 60 days, else false
                    }
                )
                .ToListAsync();

            return specializations;
        }





        public async Task<IEnumerable<Ranking>> GetAllRankingAsync()
        {
            return await _dbContext.Set<Ranking>().Take(10).ToListAsync();
        }

        public async Task<IEnumerable<Tasks>> GetAllTaskByUserIDAsync(int userId)
        {
            return await _dbContext.Set<Tasks>().Where(t => t.UserId == userId ).ToListAsync();
        }
        public async Task UpdateUserAsync(User user)
        {
            var existingUser = await _dbContext.Set<User>().FindAsync(user.UserId);
            if (existingUser == null)
            {
                throw new Exception("User not found.");
            }

            // Update the user entity with modified fields
            existingUser.FullName = user.FullName;
            existingUser.PasswordHash = user.PasswordHash;
            existingUser.ProfileImage = user.ProfileImage;
            existingUser.phone = user.phone;
            existingUser.birthday = user.birthday;
            existingUser.sex = user.sex;

            // Mark the user as modified and save changes
            _dbContext.Set<User>().Update(existingUser);
            await _dbContext.SaveChangesAsync();
        }


        public async Task<IEnumerable<Attendance>> GetUserIdAtendanceAsync(int id)
        {
            return await _dbContext.Set<Attendance>().Where(Attendance => Attendance.UserId == id).ToListAsync();
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Set<User>().FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<IEnumerable<FriendRequest>> GetFriendsListAsync(int userId)
        {
            return await _dbContext.Set<FriendRequest>()
                .Where(fr => (fr.RequesterId == userId || fr.RecipientId == userId) && fr.Status == "Accepted")
                .Select(fr => new FriendRequest
                {
                    Requester = fr.Requester, // Thông tin người gửi
                    Recipient = fr.Recipient  // Thông tin người nhận
                })
                .ToListAsync();
        }

        public async Task CreateUserAsync(User user)
        {
            _dbContext.Set<User>().Add(user);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<FriendRequest>> GetAllFriendRequestsAsync(int userId)
        {
            return await _dbContext.Set<FriendRequest>()
                .Where(fr => fr.RequesterId == userId)
                .Include(fr => fr.Requester) // Bao gồm thông tin người gửi (Requester)
                .Include(fr => fr.Recipient) // Bao gồm thông tin người nhận (Recipient)
                .ToListAsync();
        }
        public async Task<IEnumerable<FriendRequest>> GetAllFriendRecipientsAsync(int userId)
        {
            return await _dbContext.Set<FriendRequest>()
                .Where(fr => fr.RecipientId == userId)
                .Include(fr => fr.Requester) // Bao gồm thông tin người gửi (Requester)
                .Include(fr => fr.Recipient) // Bao gồm thông tin người nhận (Recipient)
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

        public class SpecializationUserDetailViewModel
        {
            public string SpecializationName { get; set; }
            public bool Status { get; set; }
        }


        public async Task<bool> CheckToken(int userid)
        {
            var tokenExists = await _dbContext.Set<RefreshToken>()
       .AnyAsync(token => token.UserId == userid );

            return tokenExists;
        }

        public async Task<IEnumerable<User>> getAlluser()
        {
            return await _dbContext.Set<User>().ToListAsync();
        }
    }
}


