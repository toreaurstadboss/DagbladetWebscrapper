using DagbladetWebscrapper.Data;
using Webscrapper.Lib;

namespace DagbladetWebscrapper;

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
			});

		builder.Services.AddMauiBlazorWebView();
		#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif

		builder.Services.AddScoped<ISummarizationUtil, SummarizationUtil>();
        builder.Services.AddScoped<IPageExtractionUtil, PageExtractionUtil>();

        builder.Services.AddSingleton<WeatherForecastService>();

		return builder.Build();
	}
}
