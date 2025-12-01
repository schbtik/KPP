using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProcureRiskAnalyzer.Client.Services;

namespace ProcureRiskAnalyzer.Client.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    private readonly IAuthService _authService;

    [ObservableProperty]
    private string username = string.Empty;

    [ObservableProperty]
    private string password = string.Empty;

    [ObservableProperty]
    private string errorMessage = string.Empty;

    public LoginViewModel(IAuthService authService, ILoadingService loadingService) 
        : base(loadingService)
    {
        _authService = authService;
    }

    [RelayCommand]
    private async Task LoginAsync()
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Please enter both username and password";
            return;
        }

        ErrorMessage = string.Empty;

        await ExecuteWithLoadingAsync(async () =>
        {
            // Ensure loading is visible before starting
            await Task.Delay(100); // Small delay to ensure UI updates
            
            var success = await _authService.LoginAsync(Username, Password);
            
            if (success)
            {
                // Ensure minimum animation time (at least 1 second total)
                await Task.Delay(700);
                
                // Clear password field for security
                Password = string.Empty;
                // Navigation will be handled by the view
                await Shell.Current.GoToAsync("//main");
            }
            else
            {
                ErrorMessage = "Invalid username or password";
            }
        });
    }
}


