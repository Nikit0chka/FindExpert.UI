using Ardalis.SharedKernel;
using FindExpert.UI.Api.Contracts;
using FindExpert.UI.Domain.Utils;
using FindExpert.UI.UseCases.Contracts;

namespace FindExpert.UI.UseCases.CQRS.Authorization.Register;

internal sealed class RegisterCommandHandler(IMasterOkApi masterOkApi, ILocalizationService localizationService):ICommandHandler<RegisterCommand, OperationResult>
{
    public async Task<OperationResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await masterOkApi.Register(new(request.Email, request.Password));

            return response.IsSuccess? OperationResult.Success() : OperationResult.Error(localizationService.GetLocalizedString(response.Error!));
        }
        catch (Exception ex)
        {
            return OperationResult.Error("Непредвиденная ошибка. Попробуйте позже.");
        }
    }
}