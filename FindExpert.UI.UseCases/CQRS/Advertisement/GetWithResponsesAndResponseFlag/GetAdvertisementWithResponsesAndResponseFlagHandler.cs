using Ardalis.SharedKernel;
using FindExpert.UI.Api.Contracts;
using FindExpert.UI.Api.Models.Advertisement.Dto;
using FindExpert.UI.Domain.Utils;
using FindExpert.UI.UseCases.Contracts;

namespace FindExpert.UI.UseCases.CQRS.Advertisement.GetWithResponsesAndResponseFlag;

internal class GetAdvertisementWithResponsesAndResponseFlagHandler(IMasterOkApi masterOkApi, ILocalizationService localizationService, ISessionService sessionService):IQueryHandler<GetAdvertisementWithResponsesAndResponseFlagQuery, OperationResult<AdvertisementInfoWithResponsesWithResponseFlag>>
{
    public async Task<OperationResult<AdvertisementInfoWithResponsesWithResponseFlag>> Handle(GetAdvertisementWithResponsesAndResponseFlagQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await masterOkApi.GetAdvertisementWithResponsesAndResponseFlagInfo(request.Id, sessionService.AccessToken!);

            return response.IsSuccess? OperationResult<AdvertisementInfoWithResponsesWithResponseFlag>.Success(response.Data!) : OperationResult<AdvertisementInfoWithResponsesWithResponseFlag>.Error(localizationService.GetLocalizedString(response.Error!));
        }
        catch (Exception)
        {
            return OperationResult<AdvertisementInfoWithResponsesWithResponseFlag>.Error("Непредвиденная ошибка. Попробуйте позже");
        }
    }
}