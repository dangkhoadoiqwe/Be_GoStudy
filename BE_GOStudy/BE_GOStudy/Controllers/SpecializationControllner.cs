using GOStudy_Logic.Service;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Model;
using System.Threading.Tasks;

namespace BE_GOStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationController : Controller
    {
        private readonly ISpecializationService _specializationService;

        // Inject SpecializationService vào controller
        public SpecializationController(ISpecializationService specializationService)
        {
            _specializationService = specializationService;
        }

        // Phương thức để trả về trang Index (mặc định)
        public IActionResult Index()
        {
            return View();
        }

        // Phương thức HTTP POST để lưu Specialization
        [HttpPost("save")]
        public async Task<IActionResult> SaveSpecialization([FromBody] Specialization specialization)
        {
            if (specialization == null || string.IsNullOrEmpty(specialization.Name))
            {
                return BadRequest("Invalid specialization data.");
            }

            // Gọi service để lưu specialization
            var result = await _specializationService.SaveSpecializationAsync(specialization);

            if (result)
            {
                return Ok("Specialization saved successfully.");
            }

            return StatusCode(500, "An error occurred while saving the specialization.");
        }
    }
}
