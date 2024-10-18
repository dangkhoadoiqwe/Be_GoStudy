using AutoMapper;
using DataAccess.Model;
using DataAccess.Repositories;
using GO_Study_Logic.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GO_Study_Logic.Service
{
    public interface IPackageService
    {
        Task<IEnumerable<PackageViewModel1>> GetAllPackagesAsync();
        Task<PackageViewModel?> GetPackageByIdAsync(int id); // Lấy package theo ID
        Task<bool> SavePackageAsync(PackageViewModel packageViewModel); // Lưu package mới
        Task<bool> UpdatePackageAsync(PackageViewModel packageViewModel); // Cập nhật package
        Task<IEnumerable<string>> GetPackageNamesByUserIdAsync(int userId);
        Task<IEnumerable<PackageViewStatusUserModel>> GetAllPackagesWithUserStatusAsync(int userId);
    }

    public class PackageService : IPackageService
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IMapper _mapper;

        public PackageService(IPackageRepository packageRepository, IMapper mapper)
        {
            _packageRepository = packageRepository;
            _mapper = mapper;
        }

        // Lấy tất cả các packages và map sang PackageViewModel
        public async Task<IEnumerable<PackageViewModel1>> GetAllPackagesAsync()
        {
            var packages = await _packageRepository.GetAllPackage();

            // Map packages to PackageViewModel using AutoMapper
            var packageViewModels = _mapper.Map<IEnumerable<PackageViewModel1>>(packages);

            return packageViewModels;
        }
        public async Task<IEnumerable<PackageViewStatusUserModel>> GetAllPackagesWithUserStatusAsync(int userId)
        {
            // Lấy tất cả các gói package
            var packages = await _packageRepository.GetAllPackage();

            // Lấy tên các package mà user hiện đang sử dụng kèm DateEnd
            var userPackagesWithDates = await _packageRepository.GetPackageNamesAndTransactionDatesByUserIdAsync(userId);

            // Map sang PackageViewStatusUserModel
            var packageViewModels = _mapper.Map<IEnumerable<PackageViewStatusUserModel>>(packages);

            // Kiểm tra và set isBlock
            foreach (var packageViewModel in packageViewModels)
            {
                // Lấy thông tin package của user (nếu có) với DateEnd
                var userPackage = userPackagesWithDates.FirstOrDefault(up => up.PackageName == packageViewModel.Name);

                // Nếu package của user đang dùng thì không khóa (isBlock = false)
                packageViewModel.isBlock = userPackage == default;

                // Set DateEnd nếu user có sử dụng package này
 

                    packageViewModel.DateEnd = packageViewModel.isBlock == false
     ? userPackage.TransactionDate.AddMonths(5)
     : DateTime.Now;
                // Lấy package tương ứng từ danh sách packages
                var matchingPackage = packages.FirstOrDefault(p => p.Name == packageViewModel.Name);

                // Kiểm tra null và lấy danh sách tính năng cho từng package
                if (matchingPackage?.Feature != null)
                {
                    // Ánh xạ tính năng
                    packageViewModel.Features = _mapper.Map<List<FeatuerViewModel>>(matchingPackage.Feature);
                }
                else
                {
                    // Khởi tạo danh sách rỗng nếu không có tính năng
                    packageViewModel.Features = new List<FeatuerViewModel>();
                }
            }



            return packageViewModels;
        }

        public async Task<IEnumerable<string>> GetPackageNamesByUserIdAsync(int userId)
        {
            return await _packageRepository.GetPackageNamesByUserIdAsync(userId);
        }

        // Lấy package theo ID và map sang PackageViewModel
        public async Task<PackageViewModel?> GetPackageByIdAsync(int id)
        {
            var package = await _packageRepository.GetPackageByIdAsync(id);
            return package == null ? null : _mapper.Map<PackageViewModel>(package);
        }

        // Lưu package mới từ PackageViewModel
        public async Task<bool> SavePackageAsync(PackageViewModel packageViewModel)
        {
            var package = _mapper.Map<Package>(packageViewModel);
            return await _packageRepository.SavePackageAsync(package);
        }

        // Cập nhật package từ PackageViewModel
        public async Task<bool> UpdatePackageAsync(PackageViewModel packageViewModel)
        {
            var package = await _packageRepository.GetPackageByIdAsync(packageViewModel.PackageId);
            if (package == null)
            {
                return false; // Package không tồn tại
            }

            // Cập nhật thông tin từ view model
            _mapper.Map(packageViewModel, package);
            return await _packageRepository.UpdatePackageAsync(package);
        }
    }
}
