using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FindExpert.UI.Api.Models.Categories.Dto;
using FindExpert.UI.Api.Models.Responses.Dto;
using FindExpert.UI.MVVM.Models.Shared;
using FindExpert.UI.UseCases.CQRS.Advertisement.Category.List;
using FindExpert.UI.UseCases.CQRS.Advertisement.GetWithResponses;
using FindExpert.UI.UseCases.CQRS.Advertisement.GetWithResponsesAndResponseFlag;
using FindExpert.UI.UseCases.CQRS.Response.Create;
using MediatR;

namespace FindExpert.UI.MVVM.ViewModels.Master;

internal sealed partial class MasterAdvertisementShowViewModel(IMediator mediator):ObservableObject, IQueryAttributable
{
    private int _id;

    [ObservableProperty] private string _name = string.Empty;
    [ObservableProperty] private string _description = string.Empty;
    [ObservableProperty] private bool _isLoading;
    [ObservableProperty] private bool _canResponse;
    [ObservableProperty] private ObservableCollection<ResponseInfo> _responses = [];
    [ObservableProperty] private ObservableCollection<CategoryInfo> _categories = [];
    [ObservableProperty] private CategoryInfo? _selectedCategory;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _id = Convert.ToInt32(query["id"].ToString());

            LoadDataCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        Responses.Clear();
        IsLoading = true;
        CanResponse = true;
        
        var categoryResult = await mediator.Send(new GetCategoryListQuery());
        var advertisementInfoResult = await mediator.Send(new GetAdvertisementWithResponsesAndResponseFlagQuery(_id));

        if (!categoryResult.IsSuccess)
        {
            WeakReferenceMessenger.Default.Send(new ToastMessage(categoryResult.ErrorMessage ?? "Ошибка загрузки данных. Попробуйте позже", ToastDuration.Long, 16));
            return;
        }

        if (!advertisementInfoResult.IsSuccess)
        {
            WeakReferenceMessenger.Default.Send(new ToastMessage(advertisementInfoResult.ErrorMessage ?? "Ошибка загрузки данных. Попробуйте позже", ToastDuration.Long, 16));
            return;
        }

        Categories = new(categoryResult.Value!);

        CanResponse = !advertisementInfoResult.Value!.CurrentUserHasResponded;
        Description = advertisementInfoResult.Value.Description;
        Name = advertisementInfoResult.Value.Name;
        SelectedCategory = categoryResult.Value!.First(category => category.Id == advertisementInfoResult.Value.CategoryId);

        foreach (var response in advertisementInfoResult.Value!.Responses)
        {
            Responses.Add(response);
        }

        IsLoading = false;
    }

    [RelayCommand]
    private async Task AddResponse()
    {
        var comment = await Shell.Current.DisplayPromptAsync(
                                                             "Добавить отклик",
                                                             "Введите ваш комментарий:",
                                                             placeholder: "Комментарий...",
                                                             maxLength: 500,
                                                             keyboard: Keyboard.Text);

        if (string.IsNullOrWhiteSpace(comment))
            return;

        var result = await mediator.Send(new CreateResponseCommand(comment, _id));

        if (!result.IsSuccess)
        {
            WeakReferenceMessenger.Default.Send(new ToastMessage(result.ErrorMessage ?? "Ошибка добавления отклика. Попробуйте позже", ToastDuration.Long, 16));
            return;
        }

        CanResponse = false;
        WeakReferenceMessenger.Default.Send(new ToastMessage("Отклик успешно добавлен!"));
        await UpdateResponses();
    }

    private async Task UpdateResponses()
    {
        var advertisementInfoResult = await mediator.Send(new GetAdvertisementWithResponsesQuery(_id));

        if (!advertisementInfoResult.IsSuccess)
        {
            WeakReferenceMessenger.Default.Send(new ToastMessage(advertisementInfoResult.ErrorMessage ?? "Ошибка загрузки данных. Попробуйте позже", ToastDuration.Long, 16));
            return;
        }

        foreach (var response in advertisementInfoResult.Value!.Responses)
        {
            Responses.Add(response);
        }
    }
}