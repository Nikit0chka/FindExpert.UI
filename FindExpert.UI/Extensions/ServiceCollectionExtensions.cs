using FindExpert.UI.Contracts;
using FindExpert.UI.MVVM.ViewModels.Customer;
using FindExpert.UI.MVVM.ViewModels.Master;
using FindExpert.UI.MVVM.ViewModels.Shared;
using FindExpert.UI.Services;
using ProfileViewModel = FindExpert.UI.MVVM.ViewModels.Customer.ProfileViewModel;
using ShowAdvertisementViewModel = FindExpert.UI.MVVM.ViewModels.Customer.ShowAdvertisementViewModel;

namespace FindExpert.UI.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddUiServices(this IServiceCollection services)
    {
        services.AddSingleton<IShellFactory, ShellFactory>();
        services.AddSingleton<IShellService, ShellService>();

        services.AddTransient<LoginViewModel>();
        services.AddTransient<RegisterViewModel>();
        services.AddTransient<ConfirmEmailViewModel>();

        AddCustomerViewModels(services);
        AddMasterViewModels(services);
    }

    private static void AddCustomerViewModels(this IServiceCollection services)
    {
        services.AddTransient<CreateAdvertisementViewModel>();
        services.AddTransient<AdvertisementListViewModel>();
        services.AddTransient<EditAdvertisementViewModel>();
        services.AddTransient<ShowAdvertisementViewModel>();
        services.AddTransient<ProfileViewModel>();
    }

    private static void AddMasterViewModels(this IServiceCollection services)
    {
        services.AddTransient<MasterAdvertisementListViewModel>();
        services.AddTransient<MasterAdvertisementShowViewModel>();
        services.AddTransient<MasterResponseListViewModel>();
        services.AddTransient<MasterProfileViewModel>();
    }
}