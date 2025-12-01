using ProcureRiskAnalyzer.Client.ViewModels;

namespace ProcureRiskAnalyzer.Client.Views;

public partial class StatisticsPage : ContentPage
{
    public StatisticsPage(StatisticsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        Loaded += async (s, e) => await viewModel.LoadStatisticsCommand.ExecuteAsync(null);
    }
}


