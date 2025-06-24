using Ardalis.SharedKernel;
using FindExpert.UI.Api.Contracts;
using FindExpert.UI.Api.Models.Responses.Dto;
using FindExpert.UI.Domain.Utils;
using FindExpert.UI.UseCases.Contracts;
using JetBrains.Annotations;

namespace FindExpert.UI.UseCases.CQRS.Response.GetMy;

[UsedImplicitly]
internal class GetMyResponseListHandler(IMasterOkApi masterOkApi, ILocalizationService localizationService, ISessionService sessionService):IQueryHandler<GetMyResponseListQuery, OperationResult<IReadOnlyCollection<ResponseInfo>>>
{
    public async Task<OperationResult<IReadOnlyCollection<ResponseInfo>>> Handle(GetMyResponseListQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await masterOkApi.GetMyResponseList(sessionService.AccessToken!);

            return response.IsSuccess? OperationResult<IReadOnlyCollection<ResponseInfo>>.Success(response.Data!.Responses) : OperationResult<IReadOnlyCollection<ResponseInfo>>.Error(localizationService.GetLocalizedString(response.Error!));
        }
        catch (Exception)
        {
            return OperationResult<IReadOnlyCollection<ResponseInfo>>.Error("Непредвиденная ошибка. Попробуйте позже");
        }
    }
}