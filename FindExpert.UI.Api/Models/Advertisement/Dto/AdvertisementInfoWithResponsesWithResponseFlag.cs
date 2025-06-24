using FindExpert.UI.Api.Models.Responses.Dto;

namespace FindExpert.UI.Api.Models.Advertisement.Dto;

public sealed record AdvertisementInfoWithResponsesWithResponseFlag(int Id, string Name, int CategoryId, string Description, bool CurrentUserHasResponded, IReadOnlyCollection<ResponseInfo> Responses);