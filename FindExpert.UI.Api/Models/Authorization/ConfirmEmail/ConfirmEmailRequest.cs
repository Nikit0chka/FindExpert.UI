namespace FindExpert.UI.Api.Models.Authorization.ConfirmEmail;

public sealed record ConfirmEmailRequest(string Email, string ConfirmationCode);