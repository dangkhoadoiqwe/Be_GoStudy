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
    public partial interface IRefreshTokenRepository : IBaseRepository<RefreshToken>
    {
    }
    public partial class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
