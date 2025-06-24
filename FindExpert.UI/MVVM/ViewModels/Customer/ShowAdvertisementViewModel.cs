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
using MediatR;

namespace FindExpert.UI.MVVM.ViewModels.Customer;

public sealed partial class ShowAdvertisementViewModel(IMediator mediator):ObservableObject, IQueryAttributable
{
    private int _id;

    [ObservableProperty] private string _name = string.Empty;
    [ObservableProperty] private string _description = string.Empty;
    [ObservableProperty] private bool _isLoading;
    [ObservableProperty] private bool _isLoadedSuccessfully;
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

        var categoryResult = await mediator.Send(new GetCategoryListQuery());
        var advertisementInfoResult = await mediator.Send(new GetAdvertisementWithResponsesQuery(_id));

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

        Description = advertisementInfoResult.Value!.Description;
        Name = advertisementInfoResult.Value.Name;
        SelectedCategory = categoryResult.Value!.First(category => category.Id == advertisementInfoResult.Value.CategoryId);

        foreach (var response in advertisementInfoResult.Value!.Responses)
        {
            Responses.Add(response);
        }

        IsLoadedSuccessfully = true;
        IsLoading = false;
    }
}