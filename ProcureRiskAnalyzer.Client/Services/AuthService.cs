using System.Net.Http.Json;
using ProcureRiskAnalyzer.Client.Models;

namespace ProcureRiskAnalyzer.Client.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiBaseUrl;
    private string? _accessToken;
    private DateTime _tokenExpiry;
    private string? _username;
    private string? _email;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        // In production, this should come from configuration
        // Change to http://localhost:5019 if using HTTP, or https://localhost:7252 for HTTPS
        _apiBaseUrl = "http://localhost:5019"; // Backend API URL
    }

    public bool IsAuthenticated => !string.IsNullOrEmpty(_accessToken) && DateTime.UtcNow < _tokenExpiry;
    public string? AccessToken => _accessToken;
    public string? Username => _username;
    public string? Email => _email;

    public async Task<bool> LoginAsync(string username, string password)
    {
        try
        {
            var loginRequest = new LoginRequest
            {
                Username = username,
                Password = password
            };

            var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/api/authapi/login", loginRequest);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
            
            if (loginResponse == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                return false;
            }

            _accessToken = loginResponse.Token;
            _tokenExpiry = loginResponse.ExpiresAt;
            _username = loginResponse.Username;
            _email = loginResponse.Email;
            
            // Set default authorization header for future requests
            _httpClient.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);
            
            return true;
        }
        catch (Exception ex)
        {
            // Log error in production
            System.Diagnostics.Debug.WriteLine($"Login error: {ex.Message}");
            return false;
        }
    }

    public Task<bool> LogoutAsync()
    {
        _accessToken = null;
        _tokenExpiry = DateTime.MinValue;
        _username = null;
        _email = null;
        _httpClient.DefaultRequestHeaders.Authorization = null;
        return Task.FromResult(true);
    }
}


