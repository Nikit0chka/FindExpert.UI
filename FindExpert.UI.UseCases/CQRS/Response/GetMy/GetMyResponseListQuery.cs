using Ardalis.SharedKernel;
using FindExpert.UI.Api.Models.Responses.Dto;
using FindExpert.UI.Domain.Utils;

namespace FindExpert.UI.UseCases.CQRS.Response.GetMy;

public readonly record struct GetMyResponseListQuery:IQuery<OperationResult<IReadOnlyCollection<ResponseInfo>>>;