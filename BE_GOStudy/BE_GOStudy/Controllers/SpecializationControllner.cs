
using Microsoft.AspNetCore.Mvc;
using DataAccess.Model;
using System.Threading.Tasks;
using GO_Study_Logic.Service;
using GO_Study_Logic.ViewModel;
using GOStudy_Logic.ViewModel;
using DataAccess.Repositories;
using GOStudy_Logic.Service.Responses;

namespace BE_GOStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationController : Controller
    {
        private readonly ISpecializationService _specializationService;
        private readonly IUserRepository _userRepository;
        public SpecializationController(ISpecializationService specializationService, IUserRepository userRepository)
        {
            _specializationService = specializationService;
            _userRepository = userRepository;
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
        [HttpPut("UpdateUserSpecialization")]
        public async Task<IActionResult> UpdateUserSpecialization([FromQuery] int userSpecializationId, [FromQuery] int specializationId)
        {
            var result = await _specializationService.UpdateSpecializationForUserAsync(userSpecializationId, specializationId);
            if (result)
            {
                return Ok(new BaseResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Specialization updated successfully.",
                    IsSuccess = true
                });
            }

            return NotFound(new BaseResponse
            {
                StatusCode = StatusCodes.Status404NotFound,
                Message = "User specialization not found.",
                IsSuccess = false
            });
        }

        [HttpPost("SaveSpecializationByUser")]
        public async Task<IActionResult> SaveUserSpecialization([FromQuery] int userId, [FromBody] IEnumerable<int> specializationIds)
        {
            // Validate input
            if (userId <= 0 || specializationIds == null || !specializationIds.Any() || specializationIds.Any(id => id <= 0))
            {
                return BadRequest("Invalid specialization data or userId. Specialization IDs must be positive integers.");
            }

            // Retrieve user information
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Determine the maximum number of specializations allowed based on Role
            int maxSpecializations = user.Role switch
            {
                1 => 2, // Role 1: max 3 rooms
                3 => 4, // Role 3: max 5 rooms
                4 => 6, // Role 4: max 7 rooms
                _ => 0  // Invalid Role, restrict to 0
            };

            if (maxSpecializations == 0)
            {
                return BadRequest("Invalid Role specified for user.");
            }

            // Get current specializations for the user
            var currentSpecializations = await _specializationService.GetallspbyUserid(userId);

            // Calculate the remaining room slots for the user
            int availableSlots = maxSpecializations - currentSpecializations.Count();
            if (availableSlots <= 0)
            {
                return BadRequest("You have already added the maximum number of rooms allowed for this role.");
            }

            // Only add the number of rooms that fit within the available slots
            var specializationsToAdd = specializationIds.Take(availableSlots);

            // Call service to save the new specializations
            var result = await _specializationService.SaveSpecializationsForUserAsync(userId, specializationsToAdd);

            if (result)
            {
                return Ok("Specializations saved successfully for the user.");
            }

            return StatusCode(500, "An error occurred while saving specializations for the user.");
        }


        [HttpGet("AvailableSpecializations")]
        public async Task<IActionResult> GetAvailableSpecializations([FromQuery] int userId)
        {
            var availableSpecializations = await _specializationService.GetAvailableSpecializationsForUserAsync(userId);

            if (!availableSpecializations.Any())
            {
                return NotFound(new BaseResponse
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "No available specializations found.",
                    IsSuccess = false
                });
            }

            return Ok(new BaseResponse
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "Available specializations retrieved successfully.",
                Data = availableSpecializations,
                IsSuccess = true
            });
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
