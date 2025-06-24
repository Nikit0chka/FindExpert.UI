using Ardalis.SharedKernel;
using FindExpert.UI.Api.Contracts;
using FindExpert.UI.Domain.Utils;
using FindExpert.UI.UseCases.Contracts;
using JetBrains.Annotations;

namespace FindExpert.UI.UseCases.CQRS.Advertisement.Delete;

[UsedImplicitly]
internal class DeleteAdvertisementHandler(IMasterOkApi masterOkApi, ILocalizationService localizationService, ISessionService sessionService):ICommandHandler<DeleteAdvertisementCommand, OperationResult>
{
    public async Task<OperationResult> Handle(DeleteAdvertisementCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await masterOkApi.DeleteAdvertisement(request.Id, sessionService.AccessToken!);

            return response.IsSuccess? OperationResult.Success() : OperationResult.Error(localizationService.GetLocalizedString(response.Error!));
        }
        catch (Exception ex)
        {
            return OperationResult.Error("Непредвиденная ошибка. Попробуйте позже");
        }
    }
}