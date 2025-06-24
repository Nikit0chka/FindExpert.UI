using Ardalis.SharedKernel;
using FindExpert.UI.Domain.Utils;

namespace FindExpert.UI.UseCases.CQRS.Authorization.ConfirmEmail;

public readonly record struct ConfirmEmailCommand(string Email, string ConfirmationCode):ICommand<OperationResult>;