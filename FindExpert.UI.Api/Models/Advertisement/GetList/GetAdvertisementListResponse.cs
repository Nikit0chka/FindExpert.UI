using FindExpert.UI.Api.Models.Advertisement.Dto;

namespace FindExpert.UI.Api.Models.Advertisement.GetList;

public sealed record GetAdvertisementListResponse(IReadOnlyCollection<AdvertisementItem> Advertisements);