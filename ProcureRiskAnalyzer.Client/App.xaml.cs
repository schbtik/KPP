namespace ProcureRiskAnalyzer.Client;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		var window = new Window(new AppShell());
		
		// Set initial route to login
		Routing.RegisterRoute("login", typeof(Views.LoginPage));
		Routing.RegisterRoute("main", typeof(Views.MainPage));
		Routing.RegisterRoute("suppliers", typeof(Views.SuppliersPage));
		Routing.RegisterRoute("tenders", typeof(Views.TendersPage));
		Routing.RegisterRoute("categories", typeof(Views.CategoriesPage));
		Routing.RegisterRoute("statistics", typeof(Views.StatisticsPage));
		Routing.RegisterRoute("about", typeof(Views.AboutPage));
		Routing.RegisterRoute("profile", typeof(Views.ProfilePage));
		
		return window;
	}
}