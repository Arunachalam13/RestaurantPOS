using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using RestaurantPosMAUI.Data;

namespace RestaurantPosMAUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("Poppins-Bold.ttf", "PoppinsBold");
				fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<DatabaseService>();

		return builder.Build();
	}
}
