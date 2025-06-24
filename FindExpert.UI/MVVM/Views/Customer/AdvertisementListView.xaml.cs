using FindExpert.UI.MVVM.ViewModels.Customer;

namespace FindExpert.UI.MVVM.Views.Customer;

internal sealed partial class AdvertisementListView
{
    public AdvertisementListView(AdvertisementListViewModel advertisementListViewModel)
    {
        InitializeComponent();

        BindingContext = advertisementListViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is AdvertisementListViewModel advertisementListViewModel)
            advertisementListViewModel.OnAppearing();
    }
}