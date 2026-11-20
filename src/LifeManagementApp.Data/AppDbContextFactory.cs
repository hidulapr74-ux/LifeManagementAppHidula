using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LifeManagementApp.Data;

// Used by dotnet ef only. The running app supplies its own device-local path.
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>();
        options.UseSqlite("Data Source=design-time.db");
        return new AppDbContext(options.Options);
    }
}
