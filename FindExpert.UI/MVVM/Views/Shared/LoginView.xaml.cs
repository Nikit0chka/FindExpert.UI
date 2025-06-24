using FindExpert.UI.MVVM.ViewModels.Shared;

namespace FindExpert.UI.MVVM.Views.Shared;

internal sealed partial class LoginView
{
    public LoginView(LoginViewModel loginViewModel)
    {
        InitializeComponent();

        BindingContext = loginViewModel;
    }
}