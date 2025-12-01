using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProcureRiskAnalyzer.Web.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace ProcureRiskAnalyzer.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthApiController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public AuthApiController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _configuration = configuration;
        _httpClient = httpClientFactory.CreateClient();
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
        {
            return BadRequest(new { message = "Username and password are required" });
        }

        try
        {
            var oktaDomain = _configuration["Okta:Domain"];
            var clientId = _configuration["Okta:ClientId"];
            var clientSecret = _configuration["Okta:ClientSecret"];

            // Крок 1: Використовуємо Okta Authentication API для перевірки облікових даних
            var authEndpoint = $"{oktaDomain}/api/v1/authn";
            
            var authRequest = new
            {
                username = request.Username,
                password = request.Password
            };

            var authRequestMessage = new HttpRequestMessage(HttpMethod.Post, authEndpoint);
            authRequestMessage.Content = new StringContent(
                JsonSerializer.Serialize(authRequest),
                Encoding.UTF8,
                "application/json");

            var authResponse = await _httpClient.SendAsync(authRequestMessage);
            var authContent = await authResponse.Content.ReadAsStringAsync();

            if (!authResponse.IsSuccessStatusCode)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            var authResult = JsonSerializer.Deserialize<OktaAuthResponse>(authContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (authResult == null || authResult.Status != "SUCCESS")
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            // Після успішної аутентифікації через Okta, генеруємо JWT токен для мобільного застосунку
            var jwtToken = GenerateJwtToken(request.Username, authResult);
            
            var email = authResult.Embedded?.User?.Profile?.Email ?? $"{request.Username}@knu.ua";
            
            return Ok(new LoginResponse
            {
                Token = jwtToken,
                Username = request.Username,
                Email = email,
                ExpiresAt = DateTime.UtcNow.AddHours(24)
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"Error during authentication: {ex.Message}" });
        }
    }

    private string GenerateJwtToken(string username, OktaAuthResponse authResult)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration["Jwt:Key"] ?? "YourSuperSecretKeyThatIsAtLeast32CharactersLongForJWTTokenSecurity!"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.NameIdentifier, authResult.Embedded?.User?.Id ?? username)
        };

        if (!string.IsNullOrEmpty(authResult.Embedded?.User?.Profile?.Email))
        {
            claims.Add(new Claim(ClaimTypes.Email, authResult.Embedded.User.Profile.Email));
        }

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"] ?? "ProcureRiskAnalyzer",
            audience: _configuration["Jwt:Audience"] ?? "ProcureRiskAnalyzer",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(24),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}

