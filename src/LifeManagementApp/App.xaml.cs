// AI-assisted code for the course project; see README.md (AI use).
using Microsoft.Extensions.DependencyInjection;

namespace LifeManagementApp;

public partial class App : Application
{
    private readonly IServiceProvider services;

    public App(IServiceProvider services)
    {
        InitializeComponent();
        this.services = services;
        UserAppTheme = AppTheme.Light;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        // Resolve the shell after App.xaml has loaded its shared colours/styles.
        // Injecting AppShell into App's constructor creates it too early.
        AppShell shell = services.GetRequiredService<AppShell>();
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
