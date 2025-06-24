using FindExpert.UI.MVVM.ViewModels.Shared;

namespace FindExpert.UI.MVVM.Views.Shared;

internal sealed partial class ConfirmEmailView
{
    public ConfirmEmailView(ConfirmEmailViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}