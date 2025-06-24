namespace FindExpert.UI.Api.Models.Responses.Dto;

public sealed record ResponseInfo(int Id, string Comment, int UserId, int AdvertisementId, DateTime ResponseDate);