using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProcureRiskAnalyzer.Client.Services;
using Microcharts;
using SkiaSharp;

namespace ProcureRiskAnalyzer.Client.ViewModels;

public partial class StatisticsViewModel : BaseViewModel
{
    private readonly IApiService _apiService;

    [ObservableProperty]
    private Chart? chart;

    [ObservableProperty]
    private Dictionary<string, decimal> statistics = new();

    [ObservableProperty]
    private List<KeyValuePair<string, decimal>> statisticsList = new();

    public StatisticsViewModel(IApiService apiService, ILoadingService loadingService) 
        : base(loadingService)
    {
        _apiService = apiService;
    }

    [RelayCommand]
    private async Task LoadStatisticsAsync()
    {
        await ExecuteWithLoadingAsync(async () =>
        {
            Statistics = await _apiService.GetTenderStatisticsAsync();
            StatisticsList = Statistics.ToList();
            UpdateChart();
        });
    }

    private void UpdateChart()
    {
        if (Statistics == null || Statistics.Count == 0)
        {
            Chart = null;
            return;
        }

        var entries = Statistics.Select((kvp, index) => new ChartEntry((float)kvp.Value)
        {
            Label = kvp.Key,
            ValueLabel = kvp.Value.ToString("C"),
            Color = GetColorForIndex(index)
        }).ToList();

        Chart = new BarChart
        {
            Entries = entries,
            BackgroundColor = SKColors.Transparent,
            LabelTextSize = 30,
            Margin = 20
        };
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("//main");
    }

    private SKColor GetColorForIndex(int index)
    {
        var colors = new[]
        {
            SKColor.Parse("#3498db"),
            SKColor.Parse("#2ecc71"),
            SKColor.Parse("#e74c3c"),
            SKColor.Parse("#f39c12"),
            SKColor.Parse("#9b59b6"),
            SKColor.Parse("#1abc9c")
        };
        return colors[index % colors.Length];
    }
}

