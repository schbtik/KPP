using ProcureRiskAnalyzer.Client.ViewModels;

namespace ProcureRiskAnalyzer.Client.Views;

public partial class SuppliersPage : ContentPage
{
    public SuppliersPage(SuppliersViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        Loaded += async (s, e) => await viewModel.LoadSuppliersCommand.ExecuteAsync(null);
    }
}


