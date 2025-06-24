using Ardalis.SharedKernel;
using FindExpert.UI.Domain.Utils;

namespace FindExpert.UI.UseCases.CQRS.Response.Create;

public readonly record struct CreateResponseCommand(string Comment, int AdvertisementId):ICommand<OperationResult>;