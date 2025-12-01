namespace ProcureRiskAnalyzer.Client;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		
		// Set initial route
		CurrentItem = Items[0]; // Login page
	}
}
