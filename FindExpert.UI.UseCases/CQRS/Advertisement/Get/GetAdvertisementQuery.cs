using Ardalis.SharedKernel;
using FindExpert.UI.Api.Models.Advertisement.Dto;
using FindExpert.UI.Domain.Utils;

namespace FindExpert.UI.UseCases.CQRS.Advertisement.Get;

public readonly record struct GetAdvertisementQuery(int Id):IQuery<OperationResult<AdvertisementInfo>>;