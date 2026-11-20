// AI-assisted code for the course project; see README.md (AI use).
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LifeManagementApp.Data;

public static class DataServiceCollectionExtensions
{
    public static IServiceCollection AddTodoData(this IServiceCollection services, string databasePath)
    {
        // Each operation receives a new, short-lived context from this factory.
        services.AddDbContextFactory<AppDbContext>(options => options.UseSqlite($"Data Source={databasePath}"));
        services.AddSingleton<DatabaseInitializer>();
        services.AddSingleton<TodoDataService>();
        return services;
    }
}
