using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace FindExpert.UI;

public static class MauiProgramExtensions
{
    public static MauiAppBuilder UseSharedMauiApp(this MauiAppBuilder builder)
    {
        builder.UseMauiApp<App>();

        builder.ConfigureFonts(static fontCollection =>
        {
            fontCollection.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fontCollection.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });

        builder.UseMauiCommunityToolkit();
#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder;
    }
}