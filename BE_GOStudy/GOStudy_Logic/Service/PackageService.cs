using AutoMapper;
using DataAccess.Repositories;
using GO_Study_Logic.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GO_Study_Logic.Service
{
    public interface IPackageService
    {
        Task<IEnumerable<PackageViewModel>> GetAllPackagesAsync();

    }
    public class PackageService : IPackageService
    {
        private IPackageRepository _packageRepository;
        private readonly IMapper _mapper;

        public PackageService(IPackageRepository packageRepository, IMapper mapper)
        {
            _packageRepository = packageRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PackageViewModel>> GetAllPackagesAsync()
        {
            var packages = await _packageRepository.GetAllPackage();
            return _mapper.Map<IEnumerable<PackageViewModel>>(packages);
        }
    }
}
