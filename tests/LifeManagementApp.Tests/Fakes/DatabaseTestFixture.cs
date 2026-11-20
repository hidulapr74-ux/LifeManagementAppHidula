// AI-assisted tests; see README.md (AI use).
using LifeManagementApp.Core.Services;
using LifeManagementApp.Data;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;

namespace LifeManagementApp.Tests.Fakes;

public class DatabaseTestFixture : IDisposable
{
    private readonly string folder = Path.Combine(Path.GetTempPath(), "life-management-tests", Guid.NewGuid().ToString("N"));
    public ServiceProvider Provider { get; }
    public ITodoRepository Repository => Provider.GetRequiredService<ITodoRepository>();

    public DatabaseTestFixture()
    {
        Directory.CreateDirectory(folder);
        Provider = OpenAnotherProvider();
    }

    public ServiceProvider OpenAnotherProvider()
    {
        var services = new ServiceCollection();
        services.AddTodoData(Path.Combine(folder, "test.db"));
        services.AddSingleton<ITodoRepository, TodoRepository>();
        return services.BuildServiceProvider();
    }

    public void Dispose()
    {
        Provider.Dispose();
        SqliteConnection.ClearAllPools();
        Directory.Delete(folder, recursive: true);
    }
}
