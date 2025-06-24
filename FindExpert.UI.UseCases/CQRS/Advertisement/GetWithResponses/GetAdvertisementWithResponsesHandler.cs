using Ardalis.SharedKernel;
using FindExpert.UI.Api.Contracts;
using FindExpert.UI.Api.Models.Advertisement.Dto;
using FindExpert.UI.Domain.Utils;
using FindExpert.UI.UseCases.Contracts;

namespace FindExpert.UI.UseCases.CQRS.Advertisement.GetWithResponses;

internal class GetAdvertisementWithResponsesHandler(IMasterOkApi masterOkApi, ILocalizationService localizationService, ISessionService sessionService):IQueryHandler<GetAdvertisementWithResponsesQuery, OperationResult<AdvertisementWithResponses>>
{
    public async Task<OperationResult<AdvertisementWithResponses>> Handle(GetAdvertisementWithResponsesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await masterOkApi.GetAdvertisementWithResponses(request.Id, sessionService.AccessToken!);

            return response.IsSuccess? OperationResult<AdvertisementWithResponses>.Success(response.Data!) : OperationResult<AdvertisementWithResponses>.Error(localizationService.GetLocalizedString(response.Error!));
        }
        catch (Exception)
        {
            return OperationResult<AdvertisementWithResponses>.Error("Непредвиденная ошибка. Попробуйте позже");
        }
    }
}