using FindExpert.UI.MVVM.ViewModels.Customer;

namespace FindExpert.UI.MVVM.Views.Customer;

internal partial class ShowAdvertisementView
{
    public ShowAdvertisementView(ShowAdvertisementViewModel createAdvertisementViewModel)
    {
        InitializeComponent();
        BindingContext = createAdvertisementViewModel;
    }
}