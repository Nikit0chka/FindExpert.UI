using FindExpert.UI.Contracts;
using FindExpert.UI.Shells;

namespace FindExpert.UI.Services;

public sealed class ShellFactory:IShellFactory
{
    public MasterShell CreateMasterShell() => new();

    public CustomerShell CreateCustomerShell() => new();

    public AuthorizationShell CreateAuthorizationShell() => new();
}