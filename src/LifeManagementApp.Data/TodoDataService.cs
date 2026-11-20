using LifeManagementApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LifeManagementApp.Data;

// AI-assisted implementation; see the README's AI use section.
public class TodoDataService
{
    private readonly IDbContextFactory<AppDbContext> factory;
    private readonly DatabaseInitializer initializer;

    public TodoDataService(IDbContextFactory<AppDbContext> factory, DatabaseInitializer initializer)
    {
        this.factory = factory;
        this.initializer = initializer;
    }

    public async Task<List<TodoEntity>> GetAllAsync()
    {
        await initializer.InitializeAsync();
        await using AppDbContext db = await factory.CreateDbContextAsync();
        return await db.Todos.AsNoTracking().Include(t => t.Category).OrderBy(t => t.Id).ToListAsync();
    }

    public async Task<TodoEntity?> GetByIdAsync(int id)
    {
        await initializer.InitializeAsync();
        await using AppDbContext db = await factory.CreateDbContextAsync();
        return await db.Todos.AsNoTracking().Include(t => t.Category).FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<CategoryEntity>> GetCategoriesAsync()
    {
        await initializer.InitializeAsync();
        await using AppDbContext db = await factory.CreateDbContextAsync();
        return await db.Categories.AsNoTracking().OrderBy(c => c.Id).ToListAsync();
    }

    public async Task<int> SaveAsync(TodoEntity item)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(item.Title);
        if (item.Title.Length > 40 || item.Notes.Length > 1000)
            throw new ArgumentException("The title or notes are too long.");
        if (item.Priority < 1 || item.Priority > 5)
            throw new ArgumentOutOfRangeException(nameof(item.Priority));

        await initializer.InitializeAsync();
        await using AppDbContext db = await factory.CreateDbContextAsync();
        if (!await db.Categories.AnyAsync(c => c.Id == item.CategoryId))
            throw new ArgumentException("Choose an existing category.");
        if (item.Id == 0)
            db.Todos.Add(item);
        else
        {
            TodoEntity existing = await db.Todos.FindAsync(item.Id)
                ?? throw new KeyNotFoundException("The task no longer exists.");
            db.Entry(existing).CurrentValues.SetValues(item);
        }
        await db.SaveChangesAsync();
        return item.Id;
    }

    public async Task SetDoneAsync(int id, bool isDone)
    {
        await initializer.InitializeAsync();
        await using AppDbContext db = await factory.CreateDbContextAsync();
        TodoEntity item = await db.Todos.FindAsync(id)
            ?? throw new KeyNotFoundException("The task no longer exists.");
        item.IsDone = isDone;
        await db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await initializer.InitializeAsync();
        await using AppDbContext db = await factory.CreateDbContextAsync();
        TodoEntity item = await db.Todos.FindAsync(id)
            ?? throw new KeyNotFoundException("The task no longer exists.");
        db.Todos.Remove(item);
        await db.SaveChangesAsync();
    }
}
