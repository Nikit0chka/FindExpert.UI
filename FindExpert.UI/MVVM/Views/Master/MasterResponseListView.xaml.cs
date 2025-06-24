using FindExpert.UI.MVVM.ViewModels.Master;

namespace FindExpert.UI.MVVM.Views.Master;

internal sealed partial class MasterResponseListView
{
    public MasterResponseListView(MasterResponseListViewModel masterAdvertisementListViewModel)
    {
        InitializeComponent();

        BindingContext = masterAdvertisementListViewModel;
    }

    protected override void OnAppearing()
    {
        if (BindingContext is MasterResponseListViewModel masterAdvertisementListViewModel)
            masterAdvertisementListViewModel.OnAppearing();
    }
}