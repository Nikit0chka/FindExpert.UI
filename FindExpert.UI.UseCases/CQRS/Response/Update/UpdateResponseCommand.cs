using Ardalis.SharedKernel;
using FindExpert.UI.Domain.Utils;

namespace FindExpert.UI.UseCases.CQRS.Response.Update;

public readonly record struct UpdateResponseCommand(int Id, string Comment):ICommand<OperationResult>;