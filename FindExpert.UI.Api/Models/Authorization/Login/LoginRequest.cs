using FindExpert.UI.Domain.AggregateModels;

namespace FindExpert.UI.Api.Models.Authorization.Login;

public sealed record LoginRequest(string Email, string Password, Role Role);