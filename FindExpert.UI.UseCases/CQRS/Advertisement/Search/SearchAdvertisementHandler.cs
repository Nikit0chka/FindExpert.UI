using Ardalis.SharedKernel;
using FindExpert.UI.Api.Contracts;
using FindExpert.UI.Api.Models.Advertisement.Dto;
using FindExpert.UI.Domain.Utils;
using FindExpert.UI.UseCases.Contracts;
using JetBrains.Annotations;

namespace FindExpert.UI.UseCases.CQRS.Advertisement.Search;

[UsedImplicitly]
internal class SearchAdvertisementHandler(IMasterOkApi masterOkApi, ILocalizationService localizationService, ISessionService sessionService):IQueryHandler<SearchAdvertisementQuery, OperationResult<IReadOnlyCollection<AdvertisementItem>>>
{
    public async Task<OperationResult<IReadOnlyCollection<AdvertisementItem>>> Handle(SearchAdvertisementQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await masterOkApi.SearchAdvertisement(request.SearchText, request.CategoryId, sessionService.AccessToken!);

            return response.IsSuccess? OperationResult<IReadOnlyCollection<AdvertisementItem>>.Success(response.Data!.Advertisements) : OperationResult<IReadOnlyCollection<AdvertisementItem>>.Error(localizationService.GetLocalizedString(response.Error!));
        }
        catch (Exception)
        {
            return OperationResult<IReadOnlyCollection<AdvertisementItem>>.Error("Непредвиденная ошибка. Попробуйте позже");
        }
    }
}