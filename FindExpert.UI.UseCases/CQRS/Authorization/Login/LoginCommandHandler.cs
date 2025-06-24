using Ardalis.SharedKernel;
using FindExpert.UI.Api.Contracts;
using FindExpert.UI.Domain.Utils;
using FindExpert.UI.UseCases.Contracts;
using FindExpert.UI.UseCases.Contracts.SecureStorageService;

namespace FindExpert.UI.UseCases.CQRS.Authorization.Login;

internal sealed class LoginCommandHandler(IMasterOkApi masterOkApi, ISecureStorageService secureStorageService, ILocalizationService localizationService):ICommandHandler<LoginCommand, OperationResult>
{
    public async Task<OperationResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await masterOkApi.Login(new(request.Email, request.Password, request.Role));

            if (!response.IsSuccess)
                return OperationResult.Error(localizationService.GetLocalizedString(response.Error!));

            await secureStorageService.SetAuthData(response.Data!.AccessToken, response.Data.RefreshToken, request.Role);

            return OperationResult.Success();
        }
        catch (Exception ex)
        {
            return OperationResult.Error("Непредвиденная ошибка. Попробуйте позже.");
        }
    }
}