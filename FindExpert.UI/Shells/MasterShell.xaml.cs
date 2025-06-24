using FindExpert.UI.MVVM.Views.Master;

namespace FindExpert.UI.Shells;

public partial class MasterShell
{
    public MasterShell()
    {
        InitializeComponent();
        RegisterRoutes();
    }

    private static void RegisterRoutes()
    {
        Routing.UnRegisterRoute("advertisement-list/profile");

        Routing.RegisterRoute("advertisement-list/advertisement-show", typeof(MasterAdvertisementShowView));
        Routing.RegisterRoute("advertisement-list/profile", typeof(MasterProfileView));
    }
}