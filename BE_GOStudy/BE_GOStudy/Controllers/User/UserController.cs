using GO_Study_Logic.Service;
using GO_Study_Logic.ViewModel.User;
using GOStudy_Logic.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BE_GO_Study.Controllers.User
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAttendanceService _attendanceService;
        public UserController(IUserService userService, IAttendanceService attendanceService)
        {
            _userService = userService;
            _attendanceService = attendanceService;
        }
        
        [HttpGet("GetUserHome/{userid}")]
        public async Task<IActionResult> GetUserHome(int userid)
        {
            var userHomeData = await _userService.GetHomeUserID(userid);

            if (userHomeData == null)
            {
                return NotFound($"User with ID {userid} not found.");
            }

            return Ok(userHomeData);
        }
        [HttpPost("SaveAttendance")]
        public async Task<IActionResult> SaveAttendance([FromQuery] AttendanceRequestModel attendanceRequest)
        {
            if (attendanceRequest == null || attendanceRequest.UserId <= 0)
            {
                return BadRequest("Invalid attendance data.");
            }

            // Lưu attendance (thêm mới hoặc cập nhật)
            await _attendanceService.SaveAttendanceAsync(attendanceRequest.UserId);

            return Ok("Attendance saved successfully.");
        }

        // Lấy danh sách điểm danh của user
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAttendance(int userId)
        {
            var attendanceList = await _attendanceService.GetAttendanceByUserIdAsync(userId);

            if (attendanceList == null || !attendanceList.Any())
            {
                return NotFound("No attendance records found.");
            }

            return Ok(attendanceList);
        }

        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] updateUserProfileModel userProfileModel, [FromQuery] int userid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _userService.UpdateUserProfileAsync(userid, userProfileModel);
                if (result)
                {
                    return Ok("User profile updated successfully.");
                }
                return BadRequest("User update failed.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("GetUserProfile/{userid}")]
        public async Task<IActionResult> GetUserProfile(int userid)
        {
            var userHomeData = await _userService.GetUserProfile(userid);

            if (userHomeData == null)
            {
                return NotFound($"User with ID {userid} not found.");
            }

            return Ok(userHomeData);
        }
    }
}
