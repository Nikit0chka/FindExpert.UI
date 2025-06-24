using Ardalis.SharedKernel;
using FindExpert.UI.Api.Contracts;
using FindExpert.UI.Domain.Utils;
using FindExpert.UI.UseCases.Contracts;

namespace FindExpert.UI.UseCases.CQRS.Authorization.ConfirmEmail;

internal class ConfirmEmailHandler(IMasterOkApi masterOkApi, ILocalizationService localizationService):ICommandHandler<ConfirmEmailCommand, OperationResult>
{
    public async Task<OperationResult> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await masterOkApi.ConfirmEmail(new(request.Email, request.ConfirmationCode));

            return response.IsSuccess? OperationResult.Success() : OperationResult.Error(localizationService.GetLocalizedString(response.Error!));
        }
        catch (Exception ex)
        {
            return OperationResult.Error("Непредвиденная ошибка. Попробуйте позже");
        }
    }
}