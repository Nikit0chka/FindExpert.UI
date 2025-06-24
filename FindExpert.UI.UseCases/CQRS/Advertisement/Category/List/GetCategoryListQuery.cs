using Ardalis.SharedKernel;
using FindExpert.UI.Api.Models.Categories.Dto;
using FindExpert.UI.Domain.Utils;

namespace FindExpert.UI.UseCases.CQRS.Advertisement.Category.List;

public readonly record struct GetCategoryListQuery:IQuery<OperationResult<IReadOnlyCollection<CategoryInfo>>>;