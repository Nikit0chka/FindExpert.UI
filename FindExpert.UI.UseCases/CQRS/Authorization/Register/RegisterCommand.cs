using Ardalis.SharedKernel;
using FindExpert.UI.Domain.Utils;

namespace FindExpert.UI.UseCases.CQRS.Authorization.Register;

public readonly record struct RegisterCommand(string Email, string Password):ICommand<OperationResult>;