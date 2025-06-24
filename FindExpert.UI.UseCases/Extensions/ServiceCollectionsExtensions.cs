using FindExpert.UI.UseCases.CQRS.Authorization.Login;
using Microsoft.Extensions.DependencyInjection;

namespace FindExpert.UI.UseCases.Extensions;

/// <summary>
///     Service collection extensions
/// </summary>
public static class ServiceCollectionsExtensions
{
    /// <summary>
    ///     Add application services to service collections
    /// </summary>
    /// <param name="serviceCollection"> Service collection </param>
    public static void AddApplicationServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(static cfg => cfg.RegisterServicesFromAssembly(typeof(LoginCommand).Assembly));
    }
}