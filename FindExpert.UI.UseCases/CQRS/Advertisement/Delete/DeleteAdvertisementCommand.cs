using Ardalis.SharedKernel;
using FindExpert.UI.Domain.Utils;

namespace FindExpert.UI.UseCases.CQRS.Advertisement.Delete;

public readonly record struct DeleteAdvertisementCommand(int Id):ICommand<OperationResult>;