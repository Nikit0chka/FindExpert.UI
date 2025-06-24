using Ardalis.SharedKernel;
using FindExpert.UI.Api.Contracts;
using FindExpert.UI.Domain.Utils;
using FindExpert.UI.UseCases.Contracts;
using JetBrains.Annotations;

namespace FindExpert.UI.UseCases.CQRS.Response.Update;

[UsedImplicitly]
internal class UpdateResponseHandler(IMasterOkApi masterOkApi, ILocalizationService localizationService, ISessionService sessionService):ICommandHandler<UpdateResponseCommand, OperationResult>
{
    public async Task<OperationResult> Handle(UpdateResponseCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await masterOkApi.UpdateResponse(request.Id, new(request.Comment), sessionService.AccessToken!);

            return response.IsSuccess? OperationResult.Success() : OperationResult.Error(localizationService.GetLocalizedString(response.Error!));
        }
        catch (Exception ex)
        {
            return OperationResult.Error("Непредвиденная ошибка. Попробуйте позже");
        }
    }
}