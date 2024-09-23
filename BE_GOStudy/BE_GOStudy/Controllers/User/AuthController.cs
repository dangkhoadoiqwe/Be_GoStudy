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

        // Validate Google token and get the token information
        var googleTokenInfo = await ValidateGoogleToken(request.IdToken);

        if (googleTokenInfo != null)
        {
            // Find or create the user in the database
            var user = await _userService.FindOrCreateUser(googleTokenInfo);  // Thay đổi từ User thành AppUser

            if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.FullName))
            {
                return BadRequest("User information is incomplete.");
            }

            // Convert Google token info to custom token format
            var customTokenInfo = ConvertToCustomFormat(googleTokenInfo);
           
                // Generate JWT token
                var appUser = _userService.ConvertToAppUser(user);

                // Generate JWT token using AppUser
                var jwtToken = _jwtService.GenerateJwtToken(user);

                // Return the token
                return Ok(new { Token = jwtToken });
            
            
        }

        return BadRequest("Invalid ID token.");
    }


    private CustomTokenInfo ConvertToCustomFormat(GoogleTokenInfo googleTokenInfo)
    {
        var customTokenInfo = new CustomTokenInfo
        {
            Context = new Context
            {
                User = new UserInfo
                {
                    Avatar = googleTokenInfo.Picture,   // Google Picture mapped to Avatar
                    Name = googleTokenInfo.Name,        // Google Name mapped to custom format
                    Email = googleTokenInfo.Email,      // Google Email mapped to custom format
                    Id = googleTokenInfo.Sub            // Google Sub (User ID) mapped to Id
                },
                Group = "a123-123-456-789"  // Hardcoded group as per your requirement
            },
            Aud = "jitsi",        // Hardcoded Audience
            Iss = "my_client",    // Hardcoded Issuer
            Sub = "meet.jit.si",  // Hardcoded Subject
            Room = "*",           // Hardcoded Room value
            Exp = googleTokenInfo.Exp  // Use the expiration from Google Token Info
        };

        return customTokenInfo;
    }

    private async Task<GoogleTokenInfo> ValidateGoogleToken(string idToken)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync($"https://oauth2.googleapis.com/tokeninfo?id_token={idToken}");

        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            // Log the raw response to check structure
            Console.WriteLine($"Google Token Response: {jsonString}");

            // Deserialize the Google token info
            var tokenInfo = JsonConvert.DeserializeObject<GoogleTokenInfo>(jsonString);
            return tokenInfo;
        }
        else
        {
            var errorResponse = await response.Content.ReadAsStringAsync();
            throw new Exception($"Token validation failed: {response.StatusCode} - {errorResponse}");
        }
    }


}

public class GoogleLoginRequest
{
    public string IdToken { get; set; }
}