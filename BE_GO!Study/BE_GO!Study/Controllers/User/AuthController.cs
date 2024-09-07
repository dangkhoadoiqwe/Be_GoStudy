using GO_Study_Logic.Service;
using GO_Study_Logic.ViewModel.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;

    public AuthController(IHttpClientFactory httpClientFactory, IUserService userService, IJwtService jwtService)
    {
        _httpClientFactory = httpClientFactory;
        _userService = userService;
        _jwtService = jwtService;
    }

    [HttpPost("google")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
    {
        var googleTokenInfo = await ValidateGoogleToken(request.IdToken);
        if (googleTokenInfo != null)
        {
            // Xác thực thành công với Google, tạo hoặc tìm user trong DB
            var user = await _userService.FindOrCreateUser(googleTokenInfo);

            // Tạo JWT token hoặc xử lý tiếp
            var jwtToken = _jwtService.GenerateToken(user);

            return Ok(new { Token = jwtToken });
        }

        return Unauthorized();
    }

    private async Task<GoogleTokenInfo> ValidateGoogleToken(string idToken)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync($"https://oauth2.googleapis.com/tokeninfo?id_token={idToken}");
        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GoogleTokenInfo>(jsonString);
        }
        return null;
    }
}

public class GoogleLoginRequest
    {
        public string IdToken { get; set; }
    }

    

 