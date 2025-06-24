using FindExpert.UI.Api.Models.Categories.Dto;

namespace FindExpert.UI.Api.Models.Categories.GetList;

public sealed record GetCategoriesResponse(IReadOnlyCollection<CategoryInfo> Categories);