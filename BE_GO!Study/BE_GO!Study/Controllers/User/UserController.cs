using GO_Study_Logic.Service;
using GO_Study_Logic.ViewModel.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BE_GO_Study.Controllers.User
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize(Roles = "Admin")]
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
