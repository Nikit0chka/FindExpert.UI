using FindExpert.UI.Api.Models.Responses.Dto;

namespace FindExpert.UI.Api.Models.Responses.GetMy;

public sealed record GetMyResponseListResponse(IReadOnlyCollection<ResponseInfo> Responses);