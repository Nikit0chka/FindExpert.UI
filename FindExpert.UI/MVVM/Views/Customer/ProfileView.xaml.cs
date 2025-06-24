using FindExpert.UI.MVVM.ViewModels.Customer;

namespace FindExpert.UI.MVVM.Views.Customer;

internal sealed partial class ProfileView
{
    public ProfileView(ProfileViewModel profileViewModel)
    {
        InitializeComponent();

        BindingContext = profileViewModel;
    }
}