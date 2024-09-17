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
                return false; // Có thể thêm throw new ArgumentNullException nếu cần thiết.
            }

            if (specialization.SpecializationId == 0)  // Nếu chưa có ID, tức là thêm mới
            {
                await _dbContext.Set<Specialization>().AddAsync(specialization);
            }
            else  // Nếu đã có ID, tức là cập nhật
            {
                _dbContext.Set<Specialization>().Update(specialization);
            }

            try
            {
                var changes = await _dbContext.SaveChangesAsync();  // Lưu thay đổi vào cơ sở dữ liệu
                return changes > 0;  // Trả về true nếu có thay đổi
            }
            catch (DbUpdateException ex)
            {
                // Log exception (nếu cần) và xử lý lỗi nếu xảy ra
                // Có thể trả về false hoặc throw exception tùy yêu cầu.
                return false;
            }
        }

    }

}
