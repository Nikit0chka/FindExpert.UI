using JetBrains.Annotations;

namespace FindExpert.UI.Api.Models.Advertisement.Dto;

[UsedImplicitly]
public sealed record AdvertisementInfo(int Id, string Name, int CategoryId, string Description);