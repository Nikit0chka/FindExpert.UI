using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FindExpert.UI.Api.Models.Advertisement.Dto;
using FindExpert.UI.MVVM.Models.Shared;
using FindExpert.UI.UseCases.CQRS.Advertisement.Delete;
using FindExpert.UI.UseCases.CQRS.Advertisement.MyList;
using MediatR;

namespace FindExpert.UI.MVVM.ViewModels.Customer;

public sealed partial class AdvertisementListViewModel(IMediator mediator):ObservableObject
{
    [ObservableProperty] private bool _isLoading;
    [ObservableProperty] private bool _isLoadedSuccessfully;
    [ObservableProperty] private ObservableCollection<AdvertisementItem> _advertisementItems = [];

    internal void OnAppearing() { LoadDataCommand.Execute(null); }

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        IsLoading = true;
        AdvertisementItems.Clear();

        var result = await mediator.Send(new GetMyAdvertisementListQuery());

        if (!result.IsSuccess)
        {
            WeakReferenceMessenger.Default.Send(new ToastMessage(result.ErrorMessage ?? "Ошибка загрузки данных. Попробуйте позже", ToastDuration.Long, 16));
            return;
        }

        foreach (var advertisement in result.Value!)
        {
            AdvertisementItems.Add(advertisement);
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
                await Shell.Current.GoToAsync($"edit-advertisement?id={id}");
                break;
            case "Удалить":
                await DeleteAdvertisementAsync(id);
                break;
        }
    }

    [RelayCommand]
    private static Task GoToCreateAdvertisementAsync() => Shell.Current.GoToAsync("create-advertisement");

    [RelayCommand]
    private static Task GoToProfileAsync() => Shell.Current.GoToAsync("profile");

    [RelayCommand]
    private static void OnAdvertisementTapped(int id) => Shell.Current.GoToAsync($"show-advertisement?id={id}");

    private async Task DeleteAdvertisementAsync(int id)
    {
        var result = await mediator.Send(new DeleteAdvertisementCommand(id));

        if (!result.IsSuccess)
        {
            WeakReferenceMessenger.Default.Send(new ToastMessage(result.ErrorMessage ?? "Ошибка удаления объявления. Попробуйте позже", ToastDuration.Long, 16));
            return;
        }

        LoadDataCommand.Execute(null);
    }
}