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
        private IUserService _userService;
        public PackageController(IPackageService packageService, IUserService userService)
        {
            _packageService = packageService;
            _userService = userService;
        }

        // Lấy tất cả các gói (packages)
        [HttpGet("All_Package")]
        public async Task<IActionResult> GetAllPackagesAsync()
        {
            var packages = await _packageService.GetAllPackagesAsync();
            if (packages == null || !packages.Any())
            {
                return NotFound("No packages found.");
            }
            return Ok(packages);
        }

        // Lấy package theo ID
      

        // Thêm package mới
        [HttpPost("Create_Package(Admin)")]
        public async Task<ActionResult> CreatePackage([FromBody] PackageViewModel packageViewModel , int userid)
        {
            var user = await _userService.GetById(userid);


            if (user.Role == 1)
            {
                var result = await _packageService.SavePackageAsync(packageViewModel);
            if (result)
            {
                return Ok("Package created successfully.");
                }
            }
            else
            {
                return StatusCode(403, "Bạn không có quyền vào");
            }

            
            return BadRequest("Failed to create package.");
        }

        // Cập nhật package hiện có
        [HttpPut("Update_Package(Admin)")]
        public async Task<ActionResult> UpdatePackage([FromBody] PackageViewModel packageViewModel, int userid)
        {
            var user = await _userService.GetById(userid);


            if (user.Role == 1)
            {
                var result = await _packageService.UpdatePackageAsync(packageViewModel);
            if (result)
            {
                return Ok("Package updated successfully.");
            }
            }
            else
            {
                return StatusCode(403, "Bạn không có quyền vào");
            }


            return BadRequest("Failed to create package.");
        }
    }
}
