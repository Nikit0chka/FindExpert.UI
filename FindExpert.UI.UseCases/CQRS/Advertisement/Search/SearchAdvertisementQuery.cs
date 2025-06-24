using Ardalis.SharedKernel;
using FindExpert.UI.Api.Models.Advertisement.Dto;
using FindExpert.UI.Domain.Utils;

namespace FindExpert.UI.UseCases.CQRS.Advertisement.Search;

public readonly record struct SearchAdvertisementQuery(int CategoryId, string SearchText):IQuery<OperationResult<IReadOnlyCollection<AdvertisementItem>>>;