using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FindExpert.UI.Contracts;
using FindExpert.UI.Domain.AggregateModels;
using FindExpert.UI.MVVM.Models.Shared;
using FindExpert.UI.UseCases.CQRS.Authorization.Login;
using MediatR;

namespace FindExpert.UI.MVVM.ViewModels.Shared;

public sealed partial class LoginViewModel(IMediator mediator, IShellService shellService):ObservableValidator
{
    [ObservableProperty] [NotifyDataErrorInfo] [Required(ErrorMessage = "Почта обязательна для заполнения")] [EmailAddress(ErrorMessage = "Неверный формат почты")]
    private string _email = string.Empty;

    [ObservableProperty] [NotifyDataErrorInfo] [Required(ErrorMessage = "Пароль обязателен")]
    private string _password = string.Empty;

    [ObservableProperty] private bool _isCustomerSelected = true;

    [ObservableProperty] private bool _isMasterSelected;

    public string? EmailError => GetErrors(nameof(Email)).FirstOrDefault()?.ErrorMessage;
    public string? PasswordError => GetErrors(nameof(Password)).FirstOrDefault()?.ErrorMessage;
    public bool HasEmailError => !string.IsNullOrEmpty(EmailError);
    public bool HasPasswordError => !string.IsNullOrEmpty(PasswordError);

    [RelayCommand]
    private async Task Login()
    {
        ValidateAllProperties();

        OnPropertyChanged(nameof(EmailError));
        OnPropertyChanged(nameof(PasswordError));
        OnPropertyChanged(nameof(HasEmailError));
        OnPropertyChanged(nameof(HasPasswordError));
        
        if (HasErrors)
        {
            var firstError = GetErrors().FirstOrDefault();

            if (firstError != null)
            {
                WeakReferenceMessenger.Default.Send(
                                                    new ToastMessage(firstError.ErrorMessage ?? "Проверьте ошибки валидации", ToastDuration.Long));
            }

            return;
        }

        var selectedRole = GetSelectedRole();

        var result = await mediator.Send(new LoginCommand(Email, Password, selectedRole));

        if (!result.IsSuccess)
        {
            WeakReferenceMessenger.Default.Send(
                                                new ToastMessage(result.ErrorMessage ?? "Ошибка входа. Попробуйте позже", ToastDuration.Long));

            return;
        }

        shellService.SetRoleBasedShell();
    }

    [RelayCommand]
    private void SelectMaster()
    {
        IsCustomerSelected = false;
        IsMasterSelected = true;
    }

    [RelayCommand]
    private void SelectCustomer()
    {
        IsCustomerSelected = true;
        IsMasterSelected = false;
    }

    private Role GetSelectedRole() => IsMasterSelected? Role.Master : Role.Customer;
}