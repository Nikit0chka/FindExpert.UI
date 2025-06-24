using FindExpert.UI.MVVM.ViewModels.Customer;

namespace FindExpert.UI.MVVM.Views.Customer;

internal sealed partial class EditAdvertisementView
{
    public EditAdvertisementView(EditAdvertisementViewModel editAdvertisementViewModel)
    {
        InitializeComponent();
        BindingContext = editAdvertisementViewModel;
    }
}