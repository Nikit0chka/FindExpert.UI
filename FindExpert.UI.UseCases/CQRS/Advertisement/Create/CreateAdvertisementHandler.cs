using Ardalis.SharedKernel;
using FindExpert.UI.Api.Contracts;
using FindExpert.UI.Domain.Utils;
using FindExpert.UI.UseCases.Contracts;
using JetBrains.Annotations;

namespace FindExpert.UI.UseCases.CQRS.Advertisement.Create;

[UsedImplicitly]
internal class CreateAdvertisementHandler(IMasterOkApi masterOkApi, ILocalizationService localizationService, ISessionService sessionService):ICommandHandler<CreateAdvertisementCommand, OperationResult>
{
    public async Task<OperationResult> Handle(CreateAdvertisementCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await masterOkApi.CreateAdvertisement(new(request.Name, request.Description, request.AdvertisementCategoryId), sessionService.AccessToken!);

            return response.IsSuccess? OperationResult.Success() : OperationResult.Error(localizationService.GetLocalizedString(response.Error!));
        }
        catch (Exception ex)
        {
            return OperationResult.Error("Непредвиденная ошибка. Попробуйте позже");
        }
    }
}