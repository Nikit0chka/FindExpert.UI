using FindExpert.UI.Domain.AggregateModels;
using FindExpert.UI.UseCases.Contracts;
using FindExpert.UI.UseCases.Contracts.SecureStorageService;

namespace FindExpert.UI.Infrastructure.Services;

internal sealed class SessionService:ISessionService
{
    private readonly ISecureStorageService _secureStorageService;

    public SessionService(ISecureStorageService secureStorageService)
    {
        _secureStorageService = secureStorageService;

        _secureStorageService.AuthDataUpdated += OnAuthDataUpdated;
    }

    public string? AccessToken { get; private set; }
    public string? RefreshToken { get; private set; }
    public Role? Role { get; private set; }

    public bool IsAuthorized => RefreshToken is not null;

    public async Task LoadFromSecureStorageAsync()
    {
        AccessToken = await _secureStorageService.GetAccessTokenAsync();
        RefreshToken = await _secureStorageService.GetRefreshTokenAsync();
        Role = await _secureStorageService.GetRoleAsync();
    }

    public void Logout()
    {
        AccessToken = null;
        RefreshToken = null;
        Role = null;
    }

    private void OnAuthDataUpdated(SecureStorageAuthDataUpdatedNotification notification)
    {
        AccessToken = notification.AccessToken;
        RefreshToken = notification.RefreshToken;
        Role = notification.Role;
    }
}