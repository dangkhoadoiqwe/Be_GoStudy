﻿using DataAccess.Model;
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
        Task<IEnumerable<string>> GetPackageNamesByUserIdAsync(int userId);
        Task<IEnumerable<(string PackageName, DateTime TransactionDate)>> GetPackageNamesAndTransactionDatesByUserIdAsync(int userId);
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
            return await _context.Packages
                .Include(p => p.Feature)  // Include related Features for each Package
                .ToListAsync();
        }
        //public async Task<IEnumerable<object>> GetPackageNamesAndTransactionDatesByUserIdAsync(int userId)
        //{
        //    return await _context.PaymentTransactions
        //        .Where(pt => pt.UserId == userId && pt.Status == "PAID")
        //        .Join(_context.Packages,
        //              pt => pt.PackageId,
        //              p => p.PackageId,
        //              (pt, p) => new
        //              {
        //                  PackageName = p.Name,
        //                  TransactionDate = pt.TransactionDate
        //              })
        //        .Distinct()
        //        .ToListAsync();
        //}
        public async Task<IEnumerable<(string PackageName, DateTime TransactionDate)>> GetPackageNamesAndTransactionDatesByUserIdAsync(int userId)
        {
            // Trả về một Anonymous Type trong truy vấn LINQ
            var result = await _context.PaymentTransactions
                .Where(pt => pt.UserId == userId && pt.Status == "PAID")
                .Join(_context.Packages,
                      pt => pt.PackageId,
                      p => p.PackageId,
                      (pt, p) => new
                      {
                          PackageName = p.Name,
                          TransactionDate = pt.TransactionDate
                      })
                .Distinct()
                .ToListAsync();

            // Chuyển đổi Anonymous Type thành tuple sau khi truy vấn hoàn tất
            return result.Select(x => (x.PackageName, x.TransactionDate));
        }


        public async Task<IEnumerable<string>> GetPackageNamesByUserIdAsync(int userId)
        {
            return await _context.PaymentTransactions
                .Where(pt => pt.UserId == userId && pt.Status == "PAID") // Lọc theo UserId và Status là "PAID"
                .Join(_context.Packages, // Join với bảng Packages
                      pt => pt.PackageId,
                      p => p.PackageId,
                      (pt, p) => p.Name) // Lấy tên Package
                .Distinct() // Loại bỏ tên trùng lặp
                .ToListAsync();
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
