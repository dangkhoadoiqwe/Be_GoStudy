using GO_Study_Logic.Service;
using GO_Study_Logic.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BE_GO_Study.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("/api/v1/users")]

    public class LoginController : ControllerBase
    {

        private readonly LoginService _loginService;

        public LoginController(LoginService loginService)
        {

            _loginService = loginService;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            var loginResult = await _loginService.Login(req);
            if (!ModelState.IsValid)
                return BadRequest();
            if (loginResult.Success)
            {
                return Ok(loginResult);
            }
            return Unauthorized();
        }
        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenModel tokenModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _loginService.VerifyAndGenerateToken(tokenModel);
                if (result == null)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            return BadRequest();
        }
    }
}