using ProcureRiskAnalyzer.Client.ViewModels;

namespace ProcureRiskAnalyzer.Client.Views;

public partial class TendersPage : ContentPage
{
    public TendersPage(TendersViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        Loaded += async (s, e) => await viewModel.LoadDataCommand.ExecuteAsync(null);
    }
}


