using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ProcureRiskAnalyzer.Client.ViewModels;

public partial class AboutViewModel : ObservableObject
{
    [ObservableProperty]
    private string appName = "Procure Risk Analyzer";

    [ObservableProperty]
    private string version = "1.0.0";

    [ObservableProperty]
    private string description = "A cross-platform application for analyzing procurement risks. Built with .NET MAUI, using MVVM pattern and Identity Server authentication.";

    [ObservableProperty]
    private string technologies = ".NET MAUI, Identity Server, Entity Framework Core, MVVM Pattern";

    [ObservableProperty]
    private string author = "Development Team";

    [RelayCommand]
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("//main");
    }
}


