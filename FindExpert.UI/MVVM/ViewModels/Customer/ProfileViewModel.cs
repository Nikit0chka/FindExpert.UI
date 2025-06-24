using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FindExpert.UI.Contracts;
using FindExpert.UI.MVVM.Models.Shared;
using FindExpert.UI.UseCases.Contracts;

namespace FindExpert.UI.MVVM.ViewModels.Customer;

internal sealed partial class ProfileViewModel(ISessionService sessionService, IShellService shellService):ObservableObject
{
    [RelayCommand]
    private void Logout()
    {
        sessionService.Logout();

        WeakReferenceMessenger.Default.Send(new ToastMessage("Выход совершен"));

        shellService.SetRoleBasedShell();
    }
}