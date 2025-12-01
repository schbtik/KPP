using ProcureRiskAnalyzer.Client.ViewModels;

namespace ProcureRiskAnalyzer.Client.Views;

public partial class AboutPage : ContentPage
{
    public AboutPage(AboutViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}


