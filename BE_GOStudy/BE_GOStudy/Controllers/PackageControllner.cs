using GO_Study_Logic.Service;
using GO_Study_Logic.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE_GOStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IPackageService _packageService;

        public PackageController(IPackageService packageService)
        {
            _packageService = packageService;
        }

        // Lấy tất cả các gói (packages)
        [HttpGet("All_Package")]
        public async Task<ActionResult<IEnumerable<PackageViewModel>>> GetAllPackagesAsync()
        {
            var packages = await _packageService.GetAllPackagesAsync();
            if (packages == null || !packages.Any())
            {
                return NotFound("No packages found.");
            }
            return Ok(packages);
        }

        // Lấy package theo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<PackageViewModel>> GetPackageById(int id)
        {
            var package = await _packageService.GetPackageByIdAsync(id);
            if (package == null)
            {
                return NotFound($"Package with ID {id} not found.");
            }
            return Ok(package);
        }

        // Thêm package mới
        [HttpPost("Create_Package")]
        public async Task<ActionResult> CreatePackage([FromBody] PackageViewModel packageViewModel)
        {
            var result = await _packageService.SavePackageAsync(packageViewModel);
            if (result)
            {
                return Ok("Package created successfully.");
            }
            return BadRequest("Failed to create package.");
        }

        // Cập nhật package hiện có
        [HttpPut("Update_Package")]
        public async Task<ActionResult> UpdatePackage([FromBody] PackageViewModel packageViewModel)
        {
            var result = await _packageService.UpdatePackageAsync(packageViewModel);
            if (result)
            {
                return Ok("Package updated successfully.");
            }
            return BadRequest("Failed to update package.");
        }
    }
}
