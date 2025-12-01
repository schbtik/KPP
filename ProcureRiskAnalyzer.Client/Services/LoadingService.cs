using System.Diagnostics;

namespace ProcureRiskAnalyzer.Client.Services;

public class LoadingService : ILoadingService
{
    private bool _isLoading;

    public bool IsLoading
    {
        get => _isLoading;
        private set
        {
            if (_isLoading != value)
            {
                _isLoading = value;
                Debug.WriteLine($"[LoadingService] IsLoading changed to: {_isLoading}");
                LoadingStateChanged?.Invoke(this, _isLoading);
            }
        }
    }

    public event EventHandler<bool>? LoadingStateChanged;

    public void ShowLoading()
    {
        Debug.WriteLine("[LoadingService] ShowLoading() called");
        IsLoading = true;
    }

    public void HideLoading()
    {
        Debug.WriteLine("[LoadingService] HideLoading() called");
        IsLoading = false;
    }
}


