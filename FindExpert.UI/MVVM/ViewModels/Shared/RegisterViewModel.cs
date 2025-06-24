using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FindExpert.UI.MVVM.Models.Shared;
using FindExpert.UI.UseCases.CQRS.Authorization.Register;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace FindExpert.UI.MVVM.ViewModels.Shared;

public sealed partial class RegisterViewModel(IMediator mediator):ObservableValidator
{
    [ObservableProperty] [NotifyDataErrorInfo] [Required(ErrorMessage = "Email обязателен")] [EmailAddress(ErrorMessage = "Неверный формат email")]
    private string _email = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Пароль обязателен")]
    [MinLength(8, ErrorMessage = "Пароль должен быть не менее 8 символов")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$",
                          ErrorMessage = "Пароль должен содержать цифры, заглавные и строчные буквы")]
    private string _password = string.Empty;

    [ObservableProperty] [NotifyDataErrorInfo] [Required(ErrorMessage = "Подтверждение пароля обязательно")] [CustomValidation(typeof(RegisterViewModel), nameof(ValidateSubmitPassword))]
    private string _submitPassword = string.Empty;

    [ObservableProperty] private bool _isCustomerSelected = true;

    [ObservableProperty] private bool _isMasterSelected;

    public string? EmailError => GetErrors(nameof(Email)).FirstOrDefault()?.ErrorMessage;
    public string? PasswordError => GetErrors(nameof(Password)).FirstOrDefault()?.ErrorMessage;
    public string? SubmitPasswordError => GetErrors(nameof(SubmitPassword)).FirstOrDefault()?.ErrorMessage;

    public bool HasEmailError => !string.IsNullOrEmpty(EmailError);
    public bool HasPasswordError => !string.IsNullOrEmpty(PasswordError);
    public bool HasSubmitPasswordError => !string.IsNullOrEmpty(SubmitPasswordError);

    [RelayCommand]
    private async Task Register()
    {
        ValidateAllProperties();

        OnPropertyChanged(nameof(EmailError));
        OnPropertyChanged(nameof(PasswordError));
        OnPropertyChanged(nameof(SubmitPasswordError));
        OnPropertyChanged(nameof(HasEmailError));
        OnPropertyChanged(nameof(HasPasswordError));
        OnPropertyChanged(nameof(HasSubmitPasswordError));

        if (HasErrors)
        {
            var firstError = GetErrors().FirstOrDefault();

            if (firstError != null)
                WeakReferenceMessenger.Default.Send(new ToastMessage(firstError.ErrorMessage ?? "Проверьте ошибки валидации", ToastDuration.Long, 16));

            return;
        }

        if (Password != SubmitPassword)
        {
            WeakReferenceMessenger.Default.Send(new ToastMessage("Пароли не совпадают", ToastDuration.Long, 16));

            return;
        }

        var result = await mediator.Send(new RegisterCommand(Email, Password));

        if (!result.IsSuccess)
        {
            WeakReferenceMessenger.Default.Send(new ToastMessage(result.ErrorMessage ?? "Ошибка, попробуйте еще раз.", ToastDuration.Long, 16));

            return;
        }

        await Shell.Current.GoToAsync($"//confirm-email?email={Email}");
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

    // Кастомная валидация для подтверждения пароля
    public static ValidationResult ValidateSubmitPassword(string submitPassword, ValidationContext context)
    {
        var instance = (RegisterViewModel) context.ObjectInstance;

        return (submitPassword == instance.Password
            ? ValidationResult.Success
            : new("Пароли не совпадают"))!;
    }

    partial void OnEmailChanged(string value)
    {
        ValidateProperty(value, nameof(Email));
        OnPropertyChanged(nameof(EmailError));
        OnPropertyChanged(nameof(HasEmailError));
    }

    partial void OnPasswordChanged(string value)
    {
        ValidateProperty(value, nameof(Password));
        OnPropertyChanged(nameof(PasswordError));
        OnPropertyChanged(nameof(HasPasswordError));
    }

    partial void OnSubmitPasswordChanged(string value)
    {
        ValidateProperty(value, nameof(SubmitPassword));
        OnPropertyChanged(nameof(SubmitPasswordError));
        OnPropertyChanged(nameof(HasSubmitPasswordError));
    }
}