using FindExpert.UI.Contracts;
using FindExpert.UI.Domain.AggregateModels;
using FindExpert.UI.UseCases.Contracts;

namespace FindExpert.UI.Services;

internal sealed class ShellService(ISessionService sessionService, IShellFactory shellFactory):IShellService
{
    public void SetMasterShell()
    {
        // if (sessionService.Role != Role.Master)
        //     throw new InvalidOperationException("Can not set master shell when session role is not master");

        Application.Current!.Windows[0].Page = shellFactory.CreateMasterShell();
    }

    public void SetCustomerShell()
    {
        // if (sessionService.Role != Role.Customer)
        //     throw new InvalidOperationException("Can not set customer shell when session role is not customer");

        Application.Current!.Windows[0].Page = shellFactory.CreateCustomerShell();
    }

    public void SetAuthorizationShell()
    {
        // if (sessionService.IsAuthorized)
        //     throw new InvalidOperationException("Can not set authorization shell when session is authorized");

        Application.Current!.Windows[0].Page = shellFactory.CreateAuthorizationShell();
    }

    public void SetRoleBasedShell()
    {
        if (sessionService.Role == Role.Master)
        {
            SetMasterShell();
            return;
        }

        if (sessionService.Role == Role.Customer)
        {
            SetCustomerShell();
            return;
        }

        SetAuthorizationShell();
    }

    public Shell GetRoleBasedShell()
    {
        return sessionService.Role?.Name switch
        {
            nameof(Role.Customer) => shellFactory.CreateCustomerShell(),
            nameof(Role.Master) => shellFactory.CreateMasterShell(),
            _ => shellFactory.CreateAuthorizationShell()
        };
    }
}