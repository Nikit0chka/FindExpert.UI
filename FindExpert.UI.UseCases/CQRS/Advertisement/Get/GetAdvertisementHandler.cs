using Ardalis.SharedKernel;
using FindExpert.UI.Api.Contracts;
using FindExpert.UI.Api.Models.Advertisement.Dto;
using FindExpert.UI.Domain.Utils;
using FindExpert.UI.UseCases.Contracts;

namespace FindExpert.UI.UseCases.CQRS.Advertisement.Get;

internal class GetAdvertisementHandler(IMasterOkApi masterOkApi, ILocalizationService localizationService, ISessionService sessionService):IQueryHandler<GetAdvertisementQuery, OperationResult<AdvertisementInfo>>
{
    public async Task<OperationResult<AdvertisementInfo>> Handle(GetAdvertisementQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await masterOkApi.GetAdvertisementInfo(request.Id, sessionService.AccessToken!);

            return response.IsSuccess? OperationResult<AdvertisementInfo>.Success(response.Data!) : OperationResult<AdvertisementInfo>.Error(localizationService.GetLocalizedString(response.Error!));
        }
        catch (Exception)
        {
            return OperationResult<AdvertisementInfo>.Error("Непредвиденная ошибка. Попробуйте позже");
        }
    }
}