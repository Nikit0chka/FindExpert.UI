using FindExpert.UI.Infrastructure.Services;
using FindExpert.UI.UseCases.Contracts;
using FindExpert.UI.UseCases.Contracts.SecureStorageService;
using Microsoft.Extensions.DependencyInjection;

namespace FindExpert.UI.Infrastructure.Extensions;

public static class ServiceCollectionsExtensions
{
    /// <summary>
    ///     Add infrastructure services to service collections
    /// </summary>
    /// <param name="serviceCollection"> Service collection </param>
    public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(static configuration => configuration.RegisterServicesFromAssembly(typeof(SessionService).Assembly));
        
        serviceCollection.AddSingleton<ISecureStorageService, SecureStorageService>();
        serviceCollection.AddSingleton<ISessionService, SessionService>();
        serviceCollection.AddSingleton<ILocalizationService, LocalizationService>();
    }
}