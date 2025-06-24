using FindExpert.UI.Domain.AggregateModels;
using FindExpert.UI.UseCases.Contracts.SecureStorageService;
using Microsoft.Maui.Storage;

namespace FindExpert.UI.Infrastructure.Services;

internal sealed class SecureStorageService:ISecureStorageService
{
    private const string RefreshTokenKey = "refreshToken";
    private const string AccessTokenKey = "accessToken";
    private const string RoleKey = "role";

    public async Task SetAuthData(string accessToken, string refreshToken, Role role)
    {
        await SecureStorage.SetAsync(AccessTokenKey, accessToken);
        await SecureStorage.SetAsync(RefreshTokenKey, refreshToken);
        await SecureStorage.SetAsync(RoleKey, role.Name);

        AuthDataUpdated?.Invoke(new(accessToken, refreshToken, role));
    }

    public async Task<Role> GetRoleAsync()
    {
        var role = await SecureStorage.GetAsync(RoleKey);

        return Role.FromName(role, true);
    }

    public async Task<string?> GetAccessTokenAsync()
    {
        try
        {
            var alo = await SecureStorage.GetAsync(AccessTokenKey);
            return alo?.ToString();

        }
        catch (Exception e)
        {
            Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            Console.WriteLine(e);
        }

        return null;
    }

    public Task<string?> GetRefreshTokenAsync() => SecureStorage.GetAsync(RefreshTokenKey);

    public Action<SecureStorageAuthDataUpdatedNotification>? AuthDataUpdated { get; set; }
}