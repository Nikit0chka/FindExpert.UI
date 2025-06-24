using FindExpert.UI.Contracts;
using FindExpert.UI.Shells;
using FindExpert.UI.UseCases.Contracts;

namespace FindExpert.UI;

public sealed partial class App
{
    private readonly ISessionService _sessionService;
    private readonly IShellService _shellService;
    private bool _isInitialized;

    public App(ISessionService sessionService, IShellService shellService)
    {
        _sessionService = sessionService;
        _shellService = shellService;
        InitializeComponent();

        //Task.Run(async () => await InitializeAsync());
    }

    protected override Window CreateWindow(IActivationState? activationState) => new( /*_isInitialized? _shellService.GetRoleBasedShell() : new LoadingShell()*/new AuthorizationShell());

    private async Task InitializeAsync()
    {
        try
        {
            await _sessionService.LoadFromSecureStorageAsync();
            _isInitialized = true;

            // После загрузки данных обновляем окно
            Dispatcher.Dispatch(SwitchToMainShell);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void SwitchToMainShell()
    {
        if (Current?.Windows[0] is { } currentWindow)
            currentWindow.Page = _shellService.GetRoleBasedShell();
    }
}