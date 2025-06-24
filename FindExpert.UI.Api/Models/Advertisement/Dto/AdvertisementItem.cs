using JetBrains.Annotations;

namespace FindExpert.UI.Api.Models.Advertisement.Dto;

[UsedImplicitly]
public sealed record AdvertisementItem(int Id, string Name, string CategoryName, string Description, int ResponseCount);