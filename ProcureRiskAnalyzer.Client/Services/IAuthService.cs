using ProcureRiskAnalyzer.Client.Models;

namespace ProcureRiskAnalyzer.Client.Services;

public interface IAuthService
{
    Task<bool> LoginAsync(string username, string password);
    Task<bool> LogoutAsync();
    bool IsAuthenticated { get; }
    string? AccessToken { get; }
    string? Username { get; }
    string? Email { get; }
}


