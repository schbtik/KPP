using ProcureRiskAnalyzer.Client.ViewModels;

namespace ProcureRiskAnalyzer.Client.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}


