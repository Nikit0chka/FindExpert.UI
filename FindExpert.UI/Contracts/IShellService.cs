namespace FindExpert.UI.Contracts;

public interface IShellService
{
    void SetMasterShell();
    void SetCustomerShell();
    void SetAuthorizationShell();
    void SetRoleBasedShell();
    Shell GetRoleBasedShell();
}