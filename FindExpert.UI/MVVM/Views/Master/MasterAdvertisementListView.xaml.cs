using FindExpert.UI.MVVM.ViewModels.Master;

namespace FindExpert.UI.MVVM.Views.Master;

internal sealed partial class MasterAdvertisementListView
{
    public MasterAdvertisementListView(MasterAdvertisementListViewModel masterAdvertisementListViewModel)
    {
        InitializeComponent();

        BindingContext = masterAdvertisementListViewModel;
    }
}