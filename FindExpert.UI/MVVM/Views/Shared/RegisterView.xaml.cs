using FindExpert.UI.MVVM.ViewModels.Shared;

namespace FindExpert.UI.MVVM.Views.Shared;

internal sealed partial class RegisterView
{
    public RegisterView(RegisterViewModel registerViewModel)
    {
        InitializeComponent();
        BindingContext = registerViewModel;
    }
}