using FindExpert.UI.MVVM.Views.Customer;

namespace FindExpert.UI.Shells;

public partial class CustomerShell
{
    public CustomerShell()
    {
        InitializeComponent();
        RegisterRoutes();
    }

    private static void RegisterRoutes()
    {
        Routing.UnRegisterRoute("advertisement-list/profile");
        Routing.RegisterRoute("advertisement-list/create-advertisement", typeof(CreateAdvertisementView));
        Routing.RegisterRoute("advertisement-list/edit-advertisement", typeof(EditAdvertisementView));
        Routing.RegisterRoute("advertisement-list/show-advertisement", typeof(ShowAdvertisementView));
        Routing.RegisterRoute("advertisement-list/profile", typeof(ProfileView));
    }
}