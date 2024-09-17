using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using NTQ.Sdk.Core.BaseConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface ISpecializationRepository : IBaseRepository<Specialization>
    {
        Task<Specialization?> GetByIdAsync(int? id);

        Task<bool> SaveSpecializationAsync(Specialization specialization);
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
    }

}
