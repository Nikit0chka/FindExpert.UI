using FindExpert.UI.MVVM.ViewModels.Master;

namespace FindExpert.UI.MVVM.Views.Master;

internal sealed partial class MasterAdvertisementShowView
{
    public MasterAdvertisementShowView(MasterAdvertisementShowViewModel masterAdvertisementListViewModel)
    {
        InitializeComponent();

        BindingContext = masterAdvertisementListViewModel;
    }
}