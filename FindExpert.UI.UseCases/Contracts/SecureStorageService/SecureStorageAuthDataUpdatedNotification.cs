using FindExpert.UI.Domain.AggregateModels;

namespace FindExpert.UI.UseCases.Contracts.SecureStorageService;

public readonly record struct SecureStorageAuthDataUpdatedNotification(
    string AccessToken,
    string RefreshToken,
    Role Role
);