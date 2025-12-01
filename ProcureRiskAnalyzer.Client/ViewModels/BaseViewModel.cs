using CommunityToolkit.Mvvm.ComponentModel;
using ProcureRiskAnalyzer.Client.Services;
using System.Diagnostics;

namespace ProcureRiskAnalyzer.Client.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    protected readonly ILoadingService LoadingService;

    [ObservableProperty]
    private bool isLoading;

    public BaseViewModel(ILoadingService loadingService)
    {
        LoadingService = loadingService;
        // Initialize with current state
        IsLoading = LoadingService.IsLoading;
        Debug.WriteLine($"[BaseViewModel] Initialized with IsLoading: {IsLoading}");
        // Subscribe to changes
        LoadingService.LoadingStateChanged += OnLoadingStateChanged;
    }

    private void OnLoadingStateChanged(object? sender, bool isLoading)
    {
        Debug.WriteLine($"[BaseViewModel] OnLoadingStateChanged called with: {isLoading}");
        // Ensure UI updates happen on main thread
        MainThread.BeginInvokeOnMainThread(() =>
        {
            IsLoading = isLoading;
            Debug.WriteLine($"[BaseViewModel] IsLoading updated to: {IsLoading} on main thread");
        });
    }

    protected async Task ExecuteWithLoadingAsync(Func<Task> action)
    {
        try
        {
            Debug.WriteLine("[BaseViewModel] ExecuteWithLoadingAsync - ShowLoading");
            LoadingService.ShowLoading();
            await action();
        }
        finally
        {
            Debug.WriteLine("[BaseViewModel] ExecuteWithLoadingAsync - HideLoading");
            LoadingService.HideLoading();
        }
    }
}


