using Ardalis.SharedKernel;
using FindExpert.UI.Api.Contracts;
using FindExpert.UI.Api.Models.Categories.Dto;
using FindExpert.UI.Domain.Utils;
using FindExpert.UI.UseCases.Contracts;

namespace FindExpert.UI.UseCases.CQRS.Advertisement.Category.List;

internal class GetCategoryListHandler(IMasterOkApi masterOkApi, ISessionService sessionService, ILocalizationService localizationService):IQueryHandler<GetCategoryListQuery, OperationResult<IReadOnlyCollection<CategoryInfo>>>
{

    public async Task<OperationResult<IReadOnlyCollection<CategoryInfo>>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await masterOkApi.GetAdvertisementCategories(sessionService.AccessToken!);

            return response.IsSuccess? OperationResult<IReadOnlyCollection<CategoryInfo>>.Success(response.Data!.Categories) : OperationResult<IReadOnlyCollection<CategoryInfo>>.Error(localizationService.GetLocalizedString(response.Error!));
        }
        catch (Exception ex)
        {
            return OperationResult<IReadOnlyCollection<CategoryInfo>>.Error("Непредвиденная ошибка. Попробуйте позже");
        }
    }
}