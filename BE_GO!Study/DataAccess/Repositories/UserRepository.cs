
using Microsoft.EntityFrameworkCore; 
using System.Linq.Expressions;
using System.Xml.Linq;
using DataAccess.Model;
using NTQ.Sdk.Core.BaseConnect;
namespace DataAccess.Repositories
{


    public partial interface IUserRepository : IBaseRepository<User>
    {
        
    }
    public partial class UserRepository : BaseRepository<User>, IUserRepository
    
    { 
        public UserRepository(DbContext dbContext) : base(dbContext)
        {

        }
      
       
    }
}


