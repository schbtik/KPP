using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProcureRiskAnalyzer.Client.Services;

namespace ProcureRiskAnalyzer.Client.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    private readonly IAuthService _authService;

    [ObservableProperty]
    private string welcomeMessage = "Welcome to Procure Risk Analyzer";

    [ObservableProperty]
    private string currentUsername = string.Empty;

    public MainViewModel(IAuthService authService, ILoadingService loadingService) 
        : base(loadingService)
    {
        _authService = authService;
        UpdateWelcomeMessage();
    }


    public void UpdateWelcomeMessage()
    {
        if (_authService.IsAuthenticated && !string.IsNullOrEmpty(_authService.Username))
        {
            CurrentUsername = _authService.Username;
            WelcomeMessage = $"Welcome, {_authService.Username}!";
        }
        else
        {
            CurrentUsername = string.Empty;
            WelcomeMessage = "Welcome to Procure Risk Analyzer";
        }
    }

    [RelayCommand]
    private async Task NavigateToSuppliersAsync()
    {
        await Shell.Current.GoToAsync("//suppliers");
    }

    [RelayCommand]
    private async Task NavigateToTendersAsync()
    {
        await Shell.Current.GoToAsync("//tenders");
    }

    [RelayCommand]
    private async Task NavigateToCategoriesAsync()
    {
        await Shell.Current.GoToAsync("//categories");
    }

    [RelayCommand]
    private async Task NavigateToStatisticsAsync()
    {
        await Shell.Current.GoToAsync("//statistics");
    }

    [RelayCommand]
    private async Task NavigateToAboutAsync()
    {
        await Shell.Current.GoToAsync("//about");
    }

    [RelayCommand]
    private async Task NavigateToProfileAsync()
    {
        await Shell.Current.GoToAsync("//profile");
    }

    [RelayCommand]
    private async Task LogoutAsync()
    {
        await _authService.LogoutAsync();
        await Shell.Current.GoToAsync("//login");
    }
}


