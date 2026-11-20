using Microsoft.EntityFrameworkCore;

namespace LifeManagementApp.Data;

// AI-assisted implementation; see the README's AI use section.
public class DatabaseInitializer
{
    private readonly IDbContextFactory<AppDbContext> factory;
    private readonly SemaphoreSlim gate = new(1, 1);
    private bool initialized;

    public DatabaseInitializer(IDbContextFactory<AppDbContext> factory) { this.factory = factory; }

    public async Task InitializeAsync()
    {
        await gate.WaitAsync();
        try
        {
            if (initialized) return;
            await using AppDbContext db = await factory.CreateDbContextAsync();
            await db.Database.MigrateAsync();
            initialized = true;
        }
        finally { gate.Release(); }
    }
}
