// AI-assisted code for the course project; see README.md (AI use).
namespace LifeManagementApp;

public partial class App : Application
{
    private readonly AppShell shell;

    public App(AppShell shell)
    {
        InitializeComponent();
        this.shell = shell;
        UserAppTheme = AppTheme.Light;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(shell) { Title = "Life Management" };
#if WINDOWS
        window.Width = 1080;
        window.Height = 840;
        window.MinimumWidth = 390;
        window.MinimumHeight = 640;
#endif
        return window;
    }
}
