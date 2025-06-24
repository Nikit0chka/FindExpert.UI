using FindExpert.UI.Shells;

namespace FindExpert.UI.Contracts;

internal interface IShellFactory
{
    MasterShell CreateMasterShell();
    CustomerShell CreateCustomerShell();
    AuthorizationShell CreateAuthorizationShell();
}