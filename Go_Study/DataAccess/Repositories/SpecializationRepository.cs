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
    }

    public partial class SpecializationRepository : BaseRepository<Specialization>, ISpecializationRepository
    {
        private readonly DbContext _dbContext;

        public SpecializationRepository(DbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Specialization?> GetByIdAsync(int? id)
        {
            return await _dbContext.Set<Specialization>().FindAsync(id);
        }
    }

    }
