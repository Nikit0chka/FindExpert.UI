using FindExpert.UI.MVVM.ViewModels.Customer;

namespace FindExpert.UI.MVVM.Views.Customer;

internal partial class CreateAdvertisementView
{
    public CreateAdvertisementView(CreateAdvertisementViewModel createAdvertisementViewModel)
    {
        InitializeComponent();
        BindingContext = createAdvertisementViewModel;
    }
}