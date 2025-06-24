using Ardalis.SharedKernel;
using FindExpert.UI.Api.Contracts;
using FindExpert.UI.Domain.Utils;
using FindExpert.UI.UseCases.Contracts;
using JetBrains.Annotations;

namespace FindExpert.UI.UseCases.CQRS.Advertisement.Update;

[UsedImplicitly]
internal class UpdateAdvertisementHandler(IMasterOkApi masterOkApi, ILocalizationService localizationService, ISessionService sessionService):ICommandHandler<UpdateAdvertisementCommand, OperationResult>
{
    public async Task<OperationResult> Handle(UpdateAdvertisementCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await masterOkApi.UpdateAdvertisement(request.Id, new(request.Name, request.Description, request.AdvertisementCategoryId), sessionService.AccessToken!);

            return response.IsSuccess? OperationResult.Success() : OperationResult.Error(localizationService.GetLocalizedString(response.Error!));
        }
        catch (Exception ex)
        {
            return OperationResult.Error("Непредвиденная ошибка. Попробуйте позже");
        }
    }
}