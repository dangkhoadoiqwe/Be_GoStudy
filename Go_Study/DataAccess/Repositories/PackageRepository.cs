using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IPackageRepository
    {
        Task<IEnumerable<Package>> GetAllPackage();
    }
    public class PackageRepository : IPackageRepository
    {
        private GOStudyContext _context;
        
        public PackageRepository(GOStudyContext context) {
            _context = context;
           }
        public async Task<IEnumerable<Package>> GetAllPackage()
        {
            return await _context.Packages.ToListAsync();
        }
    }
}
