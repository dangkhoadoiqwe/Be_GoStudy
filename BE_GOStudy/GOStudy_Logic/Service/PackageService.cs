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
