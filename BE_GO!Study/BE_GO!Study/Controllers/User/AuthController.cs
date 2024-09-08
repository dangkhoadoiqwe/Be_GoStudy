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
    private readonly LoginService _loginService;

    public AuthController(IHttpClientFactory httpClientFactory, IUserService userService, IJwtService jwtService, LoginService loginService)
    {
        _httpClientFactory = httpClientFactory;
        _userService = userService;
        _jwtService = jwtService;
        _loginService = loginService;
    }

    [HttpPost("google")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
    {
        if (string.IsNullOrEmpty(request?.IdToken))
        {
            return BadRequest("ID token is required.");
        }

        var googleTokenInfo = await ValidateGoogleToken(request.IdToken);
        if (googleTokenInfo != null)
        {
            // Successfully authenticated with Google, find or create user in DB
            var user = await _userService.FindOrCreateUser(googleTokenInfo);

            // Validate user properties before generating token
            if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.FullName))
            {
                return BadRequest("User information is incomplete.");
            }

            // Create JWT token
            var jwtToken = _jwtService.GenerateJwtToken(user);

            return Ok(new { Token = jwtToken });
        }

        return BadRequest("Invalid ID token.");
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
        else
        {
            // Log the error for debugging purposes
            var errorResponse = await response.Content.ReadAsStringAsync();
            throw new Exception($"Token validation failed: {response.StatusCode} - {errorResponse}");
        }
    }
}

public class GoogleLoginRequest
{
    public string IdToken { get; set; }
}