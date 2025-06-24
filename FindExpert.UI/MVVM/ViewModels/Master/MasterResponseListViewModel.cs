using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FindExpert.UI.Api.Models.Responses.Dto;
using FindExpert.UI.MVVM.Models.Shared;
using FindExpert.UI.UseCases.CQRS.Response.Delete;
using FindExpert.UI.UseCases.CQRS.Response.GetMy;
using FindExpert.UI.UseCases.CQRS.Response.Update;
using MediatR;

namespace FindExpert.UI.MVVM.ViewModels.Master;

internal sealed partial class MasterResponseListViewModel(IMediator mediator):ObservableObject
{
    [ObservableProperty] private bool _isLoading;
    [ObservableProperty] private bool _isLoadedSuccessfully;
    [ObservableProperty] private ObservableCollection<ResponseInfo> _responses = [];

    internal void OnAppearing() { LoadDataCommand.Execute(null); }

    [RelayCommand]
    private static Task GoToProfileAsync() => Shell.Current.GoToAsync("profile");

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        IsLoading = true;
        Responses.Clear();

        var result = await mediator.Send(new GetMyResponseListQuery());

        if (!result.IsSuccess)
        {
            WeakReferenceMessenger.Default.Send(new ToastMessage(result.ErrorMessage ?? "Ошибка загрузки данных. Попробуйте позже", ToastDuration.Long, 16));
            return;
        }

        foreach (var response in result.Value!)
        {
            Responses.Add(response);
        }

        IsLoadedSuccessfully = true;
        IsLoading = false;
    }

    [RelayCommand]
    private async Task ShowActions(int id)
    {
        var action = await Shell.Current.DisplayActionSheet(
                                                            "",
                                                            "Отмена",
                                                            null,
                                                            "Редактировать",
                                                            "Удалить");

        switch (action)
        {
            case "Редактировать":
                await EditResponse(id);
                break;
            case "Удалить":
                await DeleteResponseAsync(id);
                break;
        }
    }

    [RelayCommand]
    private void OnAdvertisementTapped(int id)
    {
        var editableResponse = Responses.FirstOrDefault(response => response.Id == id);

        if (editableResponse is null)
        {
            WeakReferenceMessenger.Default.Send(new ToastMessage("Ошибка редактирования отклика. Попробуйте позже", ToastDuration.Long, 16));
            return;
        }

        Shell.Current.GoToAsync($"advertisement-show?id={editableResponse.AdvertisementId}");
    }

    private async Task DeleteResponseAsync(int id)
    {
        var result = await mediator.Send(new DeleteResponseCommand(id));

        if (!result.IsSuccess)
        {
            WeakReferenceMessenger.Default.Send(new ToastMessage(result.ErrorMessage ?? "Ошибка удаления отклика. Попробуйте позже", ToastDuration.Long, 16));
            return;
        }

        WeakReferenceMessenger.Default.Send(new ToastMessage("Отклик успешно удален!"));
        LoadDataCommand.Execute(null);
    }

    private async Task EditResponse(int id)
    {
        var editableResponse = Responses.FirstOrDefault(response => response.Id == id);

        if (editableResponse is null)
        {
            WeakReferenceMessenger.Default.Send(new ToastMessage("Ошибка редактирования отклика. Попробуйте позже", ToastDuration.Long, 16));
            return;
        }

        var comment = await Shell.Current.DisplayPromptAsync(
                                                             "Добавить отклик",
                                                             "Введите ваш комментарий:",
                                                             placeholder: "Комментарий...",
                                                             maxLength: 500,
                                                             keyboard: Keyboard.Text,
                                                             initialValue: editableResponse.Comment);

        if (string.IsNullOrWhiteSpace(comment))
            return;

        var result = await mediator.Send(new UpdateResponseCommand(id, comment));

        if (!result.IsSuccess)
        {
            WeakReferenceMessenger.Default.Send(new ToastMessage(result.ErrorMessage ?? "Ошибка обновления отклика. Попробуйте позже", ToastDuration.Long, 16));
            return;
        }

        WeakReferenceMessenger.Default.Send(new ToastMessage("Отклик успешно обновлен!"));
        await LoadDataAsync();
    }
}