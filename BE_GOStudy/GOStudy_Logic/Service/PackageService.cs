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
        Task<IEnumerable<PackageViewModel>> GetAllPackagesAsync();
        Task<PackageViewModel?> GetPackageByIdAsync(int id); // Lấy package theo ID
        Task<bool> SavePackageAsync(PackageViewModel packageViewModel); // Lưu package mới
        Task<bool> UpdatePackageAsync(PackageViewModel packageViewModel); // Cập nhật package
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
        public async Task<IEnumerable<PackageViewModel>> GetAllPackagesAsync()
        {
            var packages = await _packageRepository.GetAllPackage();
            return _mapper.Map<IEnumerable<PackageViewModel>>(packages);
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
