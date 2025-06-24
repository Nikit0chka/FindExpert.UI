using FindExpert.UI.MVVM.ViewModels.Master;

namespace FindExpert.UI.MVVM.Views.Master;

internal sealed partial class MasterProfileView
{
    public MasterProfileView(MasterProfileViewModel profileViewModel)
    {
        InitializeComponent();

        BindingContext = profileViewModel;
    }
}