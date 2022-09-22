using OidcMobileClient.Auth0;

namespace OidcMobileClient;

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

        builder.Services.AddSingleton<MainPage>();

        builder.Services.AddSingleton(new Auth0Client(new()
        {
			Domain = "demo.duendesoftware.com",
			ClientId = "interactive.public",
			Scope = "openid profile api",
			RedirectUri = "myapp://callback"
		}));

        return builder.Build();
	}
}
