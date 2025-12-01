using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProcureRiskAnalyzer.Client.Services;

namespace ProcureRiskAnalyzer.Client.ViewModels;

public partial class ProfileViewModel : BaseViewModel
{
    private readonly IAuthService _authService;

    [ObservableProperty]
    private string username = string.Empty;

    [ObservableProperty]
    private string email = string.Empty;

    [ObservableProperty]
    private bool isAuthenticated;

    public ProfileViewModel(IAuthService authService, ILoadingService loadingService) 
        : base(loadingService)
    {
        _authService = authService;
        LoadUserInfo();
    }

    public void LoadUserInfo()
    {
        IsAuthenticated = _authService.IsAuthenticated;
        Username = _authService.Username ?? "Unknown";
        Email = _authService.Email ?? "Not available";
    }

    [RelayCommand]
    private async Task LogoutAsync()
    {
        await _authService.LogoutAsync();
        await Shell.Current.GoToAsync("//login");
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("//main");
    }
}

