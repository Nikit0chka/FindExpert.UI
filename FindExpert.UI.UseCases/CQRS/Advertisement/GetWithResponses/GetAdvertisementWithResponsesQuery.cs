using Ardalis.SharedKernel;
using FindExpert.UI.Api.Models.Advertisement.Dto;
using FindExpert.UI.Domain.Utils;

namespace FindExpert.UI.UseCases.CQRS.Advertisement.GetWithResponses;

public readonly record struct GetAdvertisementWithResponsesQuery(int Id):IQuery<OperationResult<AdvertisementWithResponses>>;