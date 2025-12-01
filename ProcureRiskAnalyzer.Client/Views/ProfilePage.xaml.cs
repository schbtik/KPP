using ProcureRiskAnalyzer.Client.ViewModels;

namespace ProcureRiskAnalyzer.Client.Views;

public partial class ProfilePage : ContentPage
{
    public ProfilePage(ProfileViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ProfileViewModel vm)
        {
            vm.LoadUserInfo();
        }
    }
}

