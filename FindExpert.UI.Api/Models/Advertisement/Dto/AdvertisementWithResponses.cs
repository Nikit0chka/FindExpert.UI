using FindExpert.UI.Api.Models.Responses.Dto;
using JetBrains.Annotations;

namespace FindExpert.UI.Api.Models.Advertisement.Dto;

[UsedImplicitly]
public sealed record AdvertisementWithResponses(int Id, string Name, int CategoryId, string Description, IReadOnlyCollection<ResponseInfo> Responses);