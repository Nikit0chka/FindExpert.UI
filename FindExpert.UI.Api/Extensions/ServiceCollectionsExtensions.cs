using FindExpert.UI.Api.Contracts;
using FindExpert.UI.Api.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FindExpert.UI.Api.Extensions;

public static class ServiceCollectionsExtensions
{
    /// <summary>
    ///     Add api services to service collections
    /// </summary>
    /// <param name="serviceCollection"> Service collection </param>
    public static void AddApiServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpClient<IMasterOkApi, MasterOkApiHttpClient>(static client =>
        {
            client.BaseAddress = new("http://95.31.204.30:8080/");
            client.Timeout = TimeSpan.FromSeconds(30);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });
    }
}