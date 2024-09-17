using GO_Study_Logic.Service;
using GO_Study_Logic.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BE_GOStudy.Controllers.Room
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassroomController : ControllerBase
    {
        private readonly IClassroomService _service;
        private readonly IUserService _userService;
        public ClassroomController(IClassroomService service, IUserService userService)
        {
            _service = service;
            _userService = userService;
        }

        [HttpGet("AllClassUser/{userid}")]
        public async Task<IActionResult> GetAllClassroomsforUserID(int userid)
        {
            try
            {
                var classrooms = await _service.GetAllClassroomsAsync(userid);
                if (classrooms == null)
                {
                    return NotFound($"No classrooms found for user with ID {userid}");
                }
                return Ok(classrooms);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("AllClassroomsAdmin/{userid}")]
        public async Task<IActionResult> GetAllClassroomsforAdmin(int userid)
        {
            try
            {
                // Lấy thông tin claims từ HttpContext.User
                var claims = HttpContext.User.Claims;
                foreach (var claim in claims)
                {
                    Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
                }

                // Lấy thông tin người dùng từ userId
                var user = await _userService.GetById(userid);
                if (user == null)
                {
                    return NotFound("User not found");
                }

                // Kiểm tra quyền của người dùng (role 2 hoặc role 3)
                if (user.Role == 2 || user.Role == 3)
                {
                    var classrooms = await _service.GetAllClassrooms();
                    if (classrooms == null || !classrooms.Any())
                    {
                        return NotFound("No classrooms found.");
                    }
                    return Ok(classrooms);
                }
                else
                {
                    // Nếu không có quyền, trả về 403 Forbidden
                    return StatusCode(403, "Bạn không có quyền vào");
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về 500 Internal Server Error
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("MeetingRoom/{userid}")]
        public async Task<IActionResult> GetMeetingRoom(int userid)
        {
            try
            {
                var classrooms = await _service.GetRoomByUserID(userid);
                if (classrooms == null)
                {
                    return NotFound($"No classrooms found for user with ID {userid}");
                }
                return Ok(classrooms);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("UpdateLinkUrl/{id}")]
        public async Task<IActionResult> UpdateClassroomLinkUrl(int id, [FromBody] string linkUrl)
        {
            if (string.IsNullOrEmpty(linkUrl))
            {
                return BadRequest("Link URL cannot be null or empty");
            }

            try
            {
                await _service.UpdateClassroomLinkUrlAsync(id, linkUrl);
                return Ok("Link URL updated successfully.");
            }
            catch (Exception ex)
            {
                // Log lỗi (ex) nếu cần
                return StatusCode(500, $"Error updating Link URL: {ex.Message}");
            }
        }

        [HttpGet("user/{userid}")]
        public async Task<ActionResult<IEnumerable<ClassroomModel>>> GetUserRooms(int userid)
        {
            var classrooms = await _service.GetUserDashboardAsync(userid);
            return Ok(classrooms);
        }

        
    }
}
