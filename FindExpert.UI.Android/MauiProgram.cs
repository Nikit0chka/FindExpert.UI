using FindExpert.UI.Api.Extensions;
using FindExpert.UI.Extensions;
using FindExpert.UI.Infrastructure.Extensions;
using FindExpert.UI.UseCases.Extensions;

namespace FindExpert.UI.Android;

/// <summary>
/// 
/// </summary>
internal static class MauiProgram
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    internal static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder.UseSharedMauiApp();

        builder.Services.AddUiServices();
        builder.Services.AddApiServices();
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices();

        return builder.Build();
    }
}