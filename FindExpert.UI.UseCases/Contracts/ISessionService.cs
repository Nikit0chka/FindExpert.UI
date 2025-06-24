using FindExpert.UI.Domain.AggregateModels;

namespace FindExpert.UI.UseCases.Contracts;

public interface ISessionService
{
    string? AccessToken { get; }
    string? RefreshToken { get; }
    Role? Role { get; }
    bool IsAuthorized { get; }
    Task LoadFromSecureStorageAsync();
    void Logout();
}