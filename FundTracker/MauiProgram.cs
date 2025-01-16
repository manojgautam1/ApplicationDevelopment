using FundTracker.Services;
using FundTracker.Services.Interface;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace FundTracker
{
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
            // service injecting
            builder.Services.AddScoped<IUser, UserService>();
            builder.Services.AddMudServices();
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
