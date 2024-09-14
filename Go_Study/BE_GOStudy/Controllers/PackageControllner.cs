using DataAccess.Model;
using GO_Study_Logic.Service;
using GO_Study_Logic.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BE_GOStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageControllner : Controller
    {
        private IPackageService _PackageService;

        public PackageControllner(IPackageService PackageService)
        {
            _PackageService = PackageService;
        }

        [HttpGet("All_Package")]
        public async Task<ActionResult<PackageViewModel>> GetAllPackagesAsync()
        {
            var blogPosts = await _PackageService.GetAllPackagesAsync();
            if (blogPosts == null)
            {
                return NotFound();
            }
            return Ok(blogPosts);
        }
    }
}
