namespace FindExpert.UI.Api.Models.Advertisement.Create;

public sealed record CreateAdvertisementRequest(string Name, string Description, int AdvertisementCategoryId);