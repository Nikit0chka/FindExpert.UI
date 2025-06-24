using FindExpert.UI.Api.Models.Advertisement.Dto;

namespace FindExpert.UI.Api.Models.Advertisement.GetMyList;

public sealed record GetMyAdvertisementListResponse(IReadOnlyCollection<AdvertisementItem> Advertisements);