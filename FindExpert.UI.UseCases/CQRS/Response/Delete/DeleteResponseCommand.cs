using Ardalis.SharedKernel;
using FindExpert.UI.Domain.Utils;

namespace FindExpert.UI.UseCases.CQRS.Response.Delete;

public readonly record struct DeleteResponseCommand(int Id):ICommand<OperationResult>;