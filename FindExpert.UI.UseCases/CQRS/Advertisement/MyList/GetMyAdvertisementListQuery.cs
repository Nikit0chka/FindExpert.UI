using Ardalis.SharedKernel;
using FindExpert.UI.Api.Models.Advertisement.Dto;
using FindExpert.UI.Domain.Utils;

namespace FindExpert.UI.UseCases.CQRS.Advertisement.MyList;

public readonly record struct GetMyAdvertisementListQuery:IQuery<OperationResult<IReadOnlyCollection<AdvertisementItem>>>;