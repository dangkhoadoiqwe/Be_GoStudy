
using Microsoft.AspNetCore.Mvc;
using DataAccess.Model;
using System.Threading.Tasks;
using GO_Study_Logic.Service;
using GO_Study_Logic.ViewModel;
using GOStudy_Logic.ViewModel;

namespace BE_GOStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationController : Controller
    {
        private readonly ISpecializationService _specializationService;

        public SpecializationController(ISpecializationService specializationService)
        {
            _specializationService = specializationService;
        }
        [HttpGet("GetAllSpecialization")]
        public async Task<ActionResult<IEnumerable<SpecializationViewModel>>> GetallSpecialization()
        {

            // Gọi service để lưu specialization
            var result = await _specializationService.GetAllSpecializationAsync();

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost("SaveSpecializationByUser")]
        public async Task<IActionResult> SaveUserSpecialization([FromQuery] int userId, [FromBody] IEnumerable<int> specializationIds)
        {
            // Validate the input data: userId must be valid, and specializationIds must be a non-empty list of positive integers
            if (userId <= 0 || specializationIds == null || !specializationIds.Any() || specializationIds.Any(id => id <= 0))
            {
                return BadRequest("Invalid specialization data or userId. Specialization IDs must be positive integers.");
            }

            // Call the service to save the specializations for the user
            var result = await _specializationService.SaveSpecializationsForUserAsync(userId, specializationIds);

            // Return appropriate response based on the result
            if (result)
            {
                return Ok("Specializations saved successfully for the user.");
            }

            return StatusCode(500, "An error occurred while saving specializations for the user.");
        }

        [HttpPost("save_Specialization")]
        public async Task<IActionResult> SaveSpecialization([FromBody] SpecializationViewModel specialization)
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
