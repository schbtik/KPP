namespace ProcureRiskAnalyzer.Client.Services;

public interface ILoadingService
{
    bool IsLoading { get; }
    event EventHandler<bool>? LoadingStateChanged;
    void ShowLoading();
    void HideLoading();
}


