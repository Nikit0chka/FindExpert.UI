using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FindExpert.UI.Api.Models.Categories.Dto;
using FindExpert.UI.MVVM.Models.Shared;
using FindExpert.UI.UseCases.CQRS.Advertisement.Category.List;
using FindExpert.UI.UseCases.CQRS.Advertisement.Create;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace FindExpert.UI.MVVM.ViewModels.Customer;

public sealed partial class CreateAdvertisementViewModel:ObservableValidator
{
    private readonly IMediator _mediator;

    [ObservableProperty] [NotifyDataErrorInfo] [Required(ErrorMessage = "Название обязательно")] [MinLength(5, ErrorMessage = "Минимум 5 символов")] [MaxLength(100, ErrorMessage = "Максимум 100 символов")]
    private string _name = string.Empty;

    [ObservableProperty] [NotifyDataErrorInfo] [Required(ErrorMessage = "Описание обязательно")] [MinLength(20, ErrorMessage = "Минимум 20 символов")] [MaxLength(1000, ErrorMessage = "Максимум 1000 символов")]
    private string _description = string.Empty;

    [ObservableProperty] private bool _isLoading;

    [ObservableProperty] private bool _isLoadedSuccessfully;

    [ObservableProperty] private ObservableCollection<CategoryInfo> _categories = [];

    [ObservableProperty] [NotifyDataErrorInfo] [Required(ErrorMessage = "Выберите категорию")]
    private CategoryInfo? _selectedCategory;

    // Свойства для привязки ошибок
    public string? NameError => GetErrors(nameof(Name)).FirstOrDefault()?.ErrorMessage;
    public string? DescriptionError => GetErrors(nameof(Description)).FirstOrDefault()?.ErrorMessage;
    public string? CategoryError => GetErrors(nameof(SelectedCategory)).FirstOrDefault()?.ErrorMessage;

    public bool HasNameError => !string.IsNullOrEmpty(NameError);
    public bool HasDescriptionError => !string.IsNullOrEmpty(DescriptionError);
    public bool HasCategoryError => !string.IsNullOrEmpty(CategoryError);

    public CreateAdvertisementViewModel(IMediator mediator)
    {
        _mediator = mediator;
        LoadDataCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        IsLoading = true;

        var result = await _mediator.Send(new GetCategoryListQuery());

        if (!result.IsSuccess)
        {
            WeakReferenceMessenger.Default.Send(
                                                new ToastMessage(result.ErrorMessage ?? "Ошибка загрузки данных. Попробуйте позже",
                                                                 ToastDuration.Long,
                                                                 16));

            return;
        }

        Categories = new(result.Value!);
        IsLoadedSuccessfully = true;
        IsLoading = false;
    }

    [RelayCommand]
    private async Task CreateAsync()
    {
        ValidateAllProperties();

        // Обновляем свойства ошибок для UI
        OnPropertyChanged(nameof(NameError));
        OnPropertyChanged(nameof(DescriptionError));
        OnPropertyChanged(nameof(CategoryError));
        OnPropertyChanged(nameof(HasNameError));
        OnPropertyChanged(nameof(HasDescriptionError));
        OnPropertyChanged(nameof(HasCategoryError));

        if (HasErrors)
        {
            var firstError = GetErrors().FirstOrDefault();

            if (firstError != null)
            {
                WeakReferenceMessenger.Default.Send(
                                                    new ToastMessage(firstError.ErrorMessage ?? "Исправьте ошибки в форме",
                                                                     ToastDuration.Long,
                                                                     16));
            }

            return;
        }

        IsLoading = true;

        var result = await _mediator.Send(
                                          new CreateAdvertisementCommand(Name, Description, SelectedCategory!.Id));

        if (!result.IsSuccess)
        {
            WeakReferenceMessenger.Default.Send(
                                                new ToastMessage(result.ErrorMessage ?? "Ошибка создания объявления. Попробуйте позже",
                                                                 ToastDuration.Long,
                                                                 16));

            return;
        }

        IsLoadedSuccessfully = true;
        ClearSelectedData();
        await Shell.Current.GoToAsync("..");
    }

    private void ClearSelectedData()
    {
        Name = string.Empty;
        Description = string.Empty;
        SelectedCategory = null;

        // Очищаем ошибки
        ClearErrors();
        OnPropertyChanged(nameof(NameError));
        OnPropertyChanged(nameof(DescriptionError));
        OnPropertyChanged(nameof(CategoryError));
    }

    // Валидация при изменении полей
    partial void OnNameChanged(string value)
    {
        ValidateProperty(value, nameof(Name));
        OnPropertyChanged(nameof(NameError));
        OnPropertyChanged(nameof(HasNameError));
    }

    partial void OnDescriptionChanged(string value)
    {
        ValidateProperty(value, nameof(Description));
        OnPropertyChanged(nameof(DescriptionError));
        OnPropertyChanged(nameof(HasDescriptionError));
    }

    partial void OnSelectedCategoryChanged(CategoryInfo? value)
    {
        ValidateProperty(value, nameof(SelectedCategory));
        OnPropertyChanged(nameof(CategoryError));
        OnPropertyChanged(nameof(HasCategoryError));
    }
}