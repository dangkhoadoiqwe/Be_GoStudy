using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using NTQ.Sdk.Core.BaseConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccess.Repositories.SpecializationRepository;

namespace DataAccess.Repositories
{
    public interface ISpecializationRepository : IBaseRepository<Specialization>
    {
        Task<Specialization?> GetByIdAsync(int? id);

        Task<bool> SaveSpecializationAsync(Specialization specialization);

        Task<bool> SaveUserSpecializationAsync(UserSpecialization userSpecialization);
        Task<IEnumerable<UserSpecializationWithDetails>> GetAllSpecializationsByUserIDAsync(int userid);
       
        Task<IEnumerable<Specialization?>> GetByUserIdAsync(int? id);
        Task<IEnumerable<Specialization>> GetAvailableSpecializationsForUserAsync(int userId);

        Task<bool> UpdateUserSpecializationAsync(int userId, int specializationId, DateTime dateStart, DateTime dateEnd);
        Task<bool> UpdateUserSpecializationAsync(int userSpecializationId, int specializationId);
        // Other methods...
        Task<IEnumerable<Specialization>> GetAllAsync();  
    }

    public partial class SpecializationRepository : BaseRepository<Specialization>, ISpecializationRepository
    {
        private readonly DbContext _dbContext;
       
        public SpecializationRepository(DbContext dbContext ) : base(dbContext)
        {
            _dbContext = dbContext;
             
        }

        public async Task<Specialization?> GetByIdAsync(int? id)
        {
            return await _dbContext.Set<Specialization>().FindAsync(id);
        }
        public async Task<IEnumerable<Specialization>> GetAllAsync()
        {
            return await _dbContext.Set<Specialization>().AsNoTracking().ToListAsync();

        }
        public async Task<IEnumerable<Specialization>> GetAvailableSpecializationsForUserAsync(int userId)
        {
            // Fetch specializations already assigned to the user
            var assignedSpecializations = await _dbContext.Set<UserSpecialization>()
                .Where(us => us.UserId == userId)
                .Select(us => us.SpecializationId)
                .ToListAsync();

            // Fetch all specializations that are not assigned to the user
            var availableSpecializations = await _dbContext.Set<Specialization>()
                .Where(s => !assignedSpecializations.Contains(s.SpecializationId))
                .ToListAsync();

            return availableSpecializations;
        }

        public async Task<bool> SaveUserSpecializationAsync(UserSpecialization userSpecialization)
        {
            await _dbContext.Set<UserSpecialization>().AddAsync(userSpecialization);

            try
            {
                var changes = await _dbContext.SaveChangesAsync();
                return changes > 0;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }
        public async Task<bool> UpdateUserSpecializationAsync(int userSpecializationId, int specializationId)
        {
            var userSpecialization = await _dbContext.Set<UserSpecialization>()
                .FirstOrDefaultAsync(us => us.UserSpecializationId == userSpecializationId);

            if (userSpecialization == null)
            {
                return false; // Not found
            }

            userSpecialization.SpecializationId = specializationId; // Update the specialization ID
            userSpecialization.DateStart = DateTime.UtcNow; // Set DateStart to now

            // Set DateEnd based on the user's role
            var user = await _dbContext.Set<User>().FindAsync(userSpecialization.UserId); // Fetch the user to get their role
            if (user != null)
            {
                userSpecialization.DateEnd = user.Role switch
                {
                    1 => userSpecialization.DateStart.AddMonths(1), // Role 1: 1 month
                    3 => userSpecialization.DateStart.AddDays(7),   // Role 3: 1 week
                    4 => userSpecialization.DateStart.AddDays(1),   // Role 4: 1 day
                    _ => userSpecialization.DateEnd // Keep existing value for other roles
                };
            }

            try
            {
                var changes = await _dbContext.SaveChangesAsync();
                return changes > 0;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }
        public async Task<bool> SaveSpecializationAsync(Specialization specialization)
        {
            if (specialization == null)
            {
                return false;
            }

            if (specialization.SpecializationId == 0)  // Thêm mới nếu ID chưa tồn tại
            {
                await _dbContext.Set<Specialization>().AddAsync(specialization);
            }
            else  // Cập nhật nếu ID đã tồn tại
            {
                _dbContext.Set<Specialization>().Update(specialization);
            }

            try
            {
                var changes = await _dbContext.SaveChangesAsync();
                return changes > 0;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }
        public async Task<bool> UpdateUserSpecializationAsync(int userId, int specializationId, DateTime dateStart, DateTime dateEnd)
        {
            var userSpecialization = await _dbContext.Set<UserSpecialization>()
                                                     .FirstOrDefaultAsync(us => us.UserId == userId && us.SpecializationId == specializationId);

            if (userSpecialization == null)
            {
                return false; // Not found
            }

            userSpecialization.DateStart = dateStart;
            userSpecialization.DateEnd = dateEnd;

            try
            {
                var changes = await _dbContext.SaveChangesAsync();
                return changes > 0;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

         
    
    //public async Task<IEnumerable<Specialization>> GetAllSpecializationsByUserIDAsync(int userid)
    //{
    //      return await _dbContext.Set<UserSpecialization>().Where(US =>US.UserId == userid).Join(
    //    _dbContext.Set<Specialization>(),
    //    us => us.SpecializationId,
    //    s => s.SpecializationId,
    //    (us, s) => s
    //).ToListAsync();
    //}
    public async Task<IEnumerable<UserSpecializationWithDetails>> GetAllSpecializationsByUserIDAsync(int userId)
        {
            return await _dbContext.Set<UserSpecialization>()
                .Where(us => us.UserId == userId)
                .Join(_dbContext.Set<Specialization>(),
                      us => us.SpecializationId,
                      s => s.SpecializationId,
                      (us, s) => new UserSpecializationWithDetails
                      {
                          UserSpecializationId = us.UserSpecializationId,
                          UserId = us.UserId,
                          SpecializationId = s.SpecializationId,
                          Name = s.Name, 
                          DateEnd = us.DateEnd
                      })
                .ToListAsync();
        }
    

    public class UserSpecializationWithDetails
    {
        public int UserSpecializationId { get; set; }
        public int UserId { get; set; }
        public int SpecializationId { get; set; }
        public string Name { get; set; }
        
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
    public Task<IEnumerable<Specialization?>> GetByUserIdAsync(int? id)
        {
            throw new NotImplementedException();
        }
    }

}
