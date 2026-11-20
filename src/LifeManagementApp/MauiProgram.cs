using LifeManagementApp.Core.Services;
using LifeManagementApp.Core.ViewModels;
using LifeManagementApp.Data;
using LifeManagementApp.Services;
using LifeManagementApp.Views;
using Microsoft.Extensions.Logging;

namespace LifeManagementApp;

// AI-assisted implementation; see the README's AI use section.
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });

        string databasePath = Path.Combine(FileSystem.AppDataDirectory, "life-management.db");
        System.Diagnostics.Debug.WriteLine($"Life Management database: {databasePath}");
        builder.Services.AddTodoData(databasePath);

        // Shared, stateless services live for the lifetime of the app.
        builder.Services.AddSingleton<ITodoRepository, TodoRepository>();
        builder.Services.AddSingleton(new HttpClient { Timeout = TimeSpan.FromSeconds(12) });
        builder.Services.AddSingleton<IWeatherService, OpenMeteoWeatherService>();
        builder.Services.AddSingleton<INavigationService, MauiNavigationService>();
        builder.Services.AddSingleton<IUserDialogService, MauiDialogService>();
        builder.Services.AddSingleton<AppShell>();

        // A fresh editor must not retain another task's unsaved form values.
        builder.Services.AddTransient<HomeViewModel>();
        builder.Services.AddTransient<TodoListViewModel>();
        builder.Services.AddTransient<AddTodoViewModel>();
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<TodoListPage>();
        builder.Services.AddTransient<AddTodoPage>();
#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}
