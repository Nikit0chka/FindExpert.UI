using FindExpert.UI.Domain.AggregateModels;

namespace FindExpert.UI.UseCases.Contracts.SecureStorageService;

public interface ISecureStorageService
{
    Task SetAuthData(string accessToken, string refreshToken, Role role);
    Task<Role> GetRoleAsync();
    Task<string?> GetAccessTokenAsync();
    Task<string?> GetRefreshTokenAsync();
    Action<SecureStorageAuthDataUpdatedNotification>? AuthDataUpdated { get; set; }
}