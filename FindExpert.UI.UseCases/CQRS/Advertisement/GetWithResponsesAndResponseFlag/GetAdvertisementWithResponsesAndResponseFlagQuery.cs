using Ardalis.SharedKernel;
using FindExpert.UI.Api.Models.Advertisement.Dto;
using FindExpert.UI.Domain.Utils;

namespace FindExpert.UI.UseCases.CQRS.Advertisement.GetWithResponsesAndResponseFlag;

public readonly record struct GetAdvertisementWithResponsesAndResponseFlagQuery(int Id):IQuery<OperationResult<AdvertisementInfoWithResponsesWithResponseFlag>>;