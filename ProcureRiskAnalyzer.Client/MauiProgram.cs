using Microsoft.Extensions.Logging;
using ProcureRiskAnalyzer.Client.Services;
using ProcureRiskAnalyzer.Client.ViewModels;
using ProcureRiskAnalyzer.Client.Views;
using ProcureRiskAnalyzer.Client.Converters;

namespace ProcureRiskAnalyzer.Client;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// Register Services
		builder.Services.AddSingleton<HttpClient>();
		builder.Services.AddSingleton<ILoadingService, LoadingService>();
		builder.Services.AddSingleton<IAuthService, AuthService>();
		builder.Services.AddSingleton<IApiService, ApiService>();

		// Register ViewModels
		builder.Services.AddTransient<LoginViewModel>();
		builder.Services.AddTransient<MainViewModel>();
		builder.Services.AddTransient<SuppliersViewModel>();
		builder.Services.AddTransient<TendersViewModel>();
		builder.Services.AddTransient<CategoriesViewModel>();
		builder.Services.AddTransient<StatisticsViewModel>();
		builder.Services.AddTransient<AboutViewModel>();
		builder.Services.AddTransient<ProfileViewModel>();

		// Register Views
		builder.Services.AddTransient<LoginPage>();
		builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<SuppliersPage>();
		builder.Services.AddTransient<TendersPage>();
		builder.Services.AddTransient<CategoriesPage>();
		builder.Services.AddTransient<StatisticsPage>();
		builder.Services.AddTransient<AboutPage>();
		builder.Services.AddTransient<ProfilePage>();

		// Register Converters
		builder.Services.AddSingleton<BoolToTextConverter>();
		builder.Services.AddSingleton<IsStringNotNullOrEmptyConverter>();
		builder.Services.AddSingleton<BoolToColorConverter>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
