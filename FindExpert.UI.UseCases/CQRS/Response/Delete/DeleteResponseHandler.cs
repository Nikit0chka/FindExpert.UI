using Ardalis.SharedKernel;
using FindExpert.UI.Api.Contracts;
using FindExpert.UI.Domain.Utils;
using FindExpert.UI.UseCases.Contracts;
using JetBrains.Annotations;

namespace FindExpert.UI.UseCases.CQRS.Response.Delete;

[UsedImplicitly]
internal class DeleteResponseHandler(IMasterOkApi masterOkApi, ILocalizationService localizationService, ISessionService sessionService):ICommandHandler<DeleteResponseCommand, OperationResult>
{
    public async Task<OperationResult> Handle(DeleteResponseCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await masterOkApi.DeleteResponse(request.Id, sessionService.AccessToken!);

            return response.IsSuccess? OperationResult.Success() : OperationResult.Error(localizationService.GetLocalizedString(response.Error!));
        }
        catch (Exception ex)
        {
            return OperationResult.Error("Непредвиденная ошибка. Попробуйте позже");
        }
    }
}