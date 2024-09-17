using DataAccess.Model;
using NTQ.Sdk.Core.BaseConnect;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public partial interface ISemestersRepository : IBaseRepository<Semester>
    {
        Task<Semester?> GetByIdAsync(int? id);
         
    }

    public partial class SemestersRepository : BaseRepository<Semester>, ISemestersRepository
    {
        private readonly DbContext _dbContext;

        public SemestersRepository(DbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        

        public async Task<Semester?> GetByIdAsync(int? semesterId)
        {
            return await _dbContext.Set<Semester>().FindAsync(semesterId);
        }
    }
}