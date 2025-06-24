using Ardalis.SharedKernel;
using FindExpert.UI.Domain.AggregateModels;
using FindExpert.UI.Domain.Utils;

namespace FindExpert.UI.UseCases.CQRS.Authorization.Login;

public readonly record struct LoginCommand(string Email, string Password, Role Role):ICommand<OperationResult>;