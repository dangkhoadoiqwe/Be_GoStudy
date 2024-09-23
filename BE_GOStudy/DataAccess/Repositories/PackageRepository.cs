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
        Task<Package?> GetPackageByIdAsync(int id); 
        Task<bool> SavePackageAsync(Package package); 
        Task<bool> UpdatePackageAsync(Package package);
     //   Task<bool> CheckPaymentstatus(long code);
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
        public async Task<Package?> GetPackageByIdAsync(int id)
        {
            return await _context.Packages.FindAsync(id);
        }

        // Lưu package mới
        public async Task<bool> SavePackageAsync(Package package)
        {
            if (package == null)
            {
                return false;
            }

            await _context.Packages.AddAsync(package);
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        // Cập nhật package đã tồn tại
        public async Task<bool> UpdatePackageAsync(Package package)
        {
            if (package == null)
            {
                return false;
            }

            _context.Packages.Update(package);
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }
    }
}
