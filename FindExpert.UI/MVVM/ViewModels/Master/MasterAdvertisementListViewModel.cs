using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FindExpert.UI.Api.Models.Advertisement.Dto;
using FindExpert.UI.Api.Models.Categories.Dto;
using FindExpert.UI.MVVM.Models.Shared;
using FindExpert.UI.UseCases.CQRS.Advertisement.Category.List;
using FindExpert.UI.UseCases.CQRS.Advertisement.Search;
using MediatR;

namespace FindExpert.UI.MVVM.ViewModels.Master;

internal partial class MasterAdvertisementListViewModel:ObservableObject
{
    [ObservableProperty] private string _searchText = string.Empty;
    [ObservableProperty] private bool _isRefreshing;
    [ObservableProperty] private ObservableCollection<AdvertisementItem> _advertisementItems = [];
    [ObservableProperty] private ObservableCollection<CategoryInfo> _categories = [];
    [ObservableProperty] private CategoryInfo? _selectedCategory;

    private readonly IMediator _mediator;
    private bool _isInitialized;

    private AsyncRelayCommand LoadDataCommand { get; }


    public MasterAdvertisementListViewModel(IMediator mediator)
    {
        _mediator = mediator;
        LoadDataCommand = new(LoadInitialDataAsync);
        LoadDataCommand.Execute(null);
    }

    partial void OnSearchTextChanged(string value)
    {
        if (!_isInitialized)
            return;

        if (string.IsNullOrEmpty(value))
            Task.Run(async () => await LoadFilteredDataAsync());
    }

    partial void OnSelectedCategoryChanged(CategoryInfo? value)
    {
        if (_isInitialized)
            Task.Run(async () => await LoadFilteredDataAsync());
    }


    [RelayCommand]
    private Task Search() => !_isInitialized? Task.CompletedTask : LoadFilteredDataAsync();

    [RelayCommand]
    private Task ResetFilters()
    {
        SelectedCategory = null;
        SearchText = string.Empty;
        return LoadFilteredDataAsync();
    }

    [RelayCommand]
    private async Task Refresh()
    {
        if (IsRefreshing) return;

        IsRefreshing = true;
        await LoadFilteredDataAsync();
        IsRefreshing = false;
    }

    [RelayCommand]
    private static void AdvertisementTapped(int id) => Shell.Current.GoToAsync($"advertisement-show?id={id}");

    [RelayCommand]
    private static Task GoToProfileAsync() => Shell.Current.GoToAsync("profile");

    private async Task LoadInitialDataAsync()
    {
        var searchResult = await _mediator.Send(new SearchAdvertisementQuery(0, ""));
        var categoriesResult = await _mediator.Send(new GetCategoryListQuery());

        if (!categoriesResult.IsSuccess || !searchResult.IsSuccess)
        {
            WeakReferenceMessenger.Default.Send(new ToastMessage(
                                                                 categoriesResult.ErrorMessage ?? "Ошибка загрузки данных. Попробуйте позже",
                                                                 ToastDuration.Long,
                                                                 16));

            return;
        }

        AdvertisementItems = new(searchResult.Value!);
        Categories = new(categoriesResult.Value!);
        _isInitialized = true;
    }

    private async Task LoadFilteredDataAsync()
    {
        var searchResult = await _mediator.Send(
                                                new SearchAdvertisementQuery(SelectedCategory?.Id ?? 0, SearchText));

        if (!searchResult.IsSuccess)
        {
            WeakReferenceMessenger.Default.Send(new ToastMessage(
                                                                 searchResult.ErrorMessage ?? "Ошибка загрузки данных. Попробуйте позже",
                                                                 ToastDuration.Long,
                                                                 16));

            return;
        }

        AdvertisementItems = new(searchResult.Value!);
    }
}