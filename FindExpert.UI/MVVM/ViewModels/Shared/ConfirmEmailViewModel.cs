using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FindExpert.UI.Contracts;
using FindExpert.UI.MVVM.Models.Shared;
using FindExpert.UI.UseCases.CQRS.Authorization.ConfirmEmail;
using MediatR;

namespace FindExpert.UI.MVVM.ViewModels.Shared;

public sealed partial class ConfirmEmailViewModel(IMediator mediator, IShellService shellService):ObservableObject, IQueryAttributable
{
    [ObservableProperty] private string _confirmationCode = string.Empty;
    [ObservableProperty] private string _email = string.Empty;

    [RelayCommand]
    private async Task Confirm()
    {
        var result = await mediator.Send(new ConfirmEmailCommand(Email, ConfirmationCode));

        if (!result.IsSuccess)
        {
            WeakReferenceMessenger.Default.Send(new ToastMessage(result.ErrorMessage ?? "Ошибка подтверждения. Попробуйте позже", ToastDuration.Long, 16));
            return;
        }

        shellService.SetRoleBasedShell();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query) { Email = query["email"].ToString()!; }
}