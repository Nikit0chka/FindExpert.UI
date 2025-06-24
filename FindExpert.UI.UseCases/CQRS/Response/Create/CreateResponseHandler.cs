using Ardalis.SharedKernel;
using FindExpert.UI.Api.Contracts;
using FindExpert.UI.Domain.Utils;
using FindExpert.UI.UseCases.Contracts;
using JetBrains.Annotations;

namespace FindExpert.UI.UseCases.CQRS.Response.Create;

[UsedImplicitly]
internal class CreateResponseHandler(IMasterOkApi masterOkApi, ILocalizationService localizationService, ISessionService sessionService):ICommandHandler<CreateResponseCommand, OperationResult>
{
    public async Task<OperationResult> Handle(CreateResponseCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await masterOkApi.CreateResponse(new(request.Comment, request.AdvertisementId), sessionService.AccessToken!);

            return response.IsSuccess? OperationResult.Success() : OperationResult.Error(localizationService.GetLocalizedString(response.Error!));
        }
        catch (Exception ex)
        {
            return OperationResult.Error("Непредвиденная ошибка. Попробуйте позже");
        }
    }
}