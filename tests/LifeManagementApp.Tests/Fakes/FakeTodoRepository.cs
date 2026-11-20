using LifeManagementApp.Core.Models;
using LifeManagementApp.Core.Services;

namespace LifeManagementApp.Tests.Fakes;

public class FakeTodoRepository : ITodoRepository
{
    public List<TodoItem> Items { get; } = new();
    public bool FailWrites { get; set; }
    public int SaveCalls { get; private set; }

    public Task<List<TodoItem>> GetAllAsync() => Task.FromResult(Items.Select(Clone).ToList());
    public Task<TodoItem?> GetByIdAsync(int id) => Task.FromResult(Items.Where(t => t.Id == id).Select(Clone).FirstOrDefault());
    public Task<List<CategoryItem>> GetCategoriesAsync() => Task.FromResult(new List<CategoryItem>
    {
        new() { Id = 1, Name = "Study" }, new() { Id = 2, Name = "Work" }
    });

    public Task SaveAsync(TodoItem item)
    {
        if (FailWrites) throw new IOException("Simulated storage failure");
        SaveCalls++;
        if (item.Id == 0) item.Id = Items.Select(t => t.Id).DefaultIfEmpty().Max() + 1;
        Items.RemoveAll(t => t.Id == item.Id);
        Items.Add(Clone(item));
        return Task.CompletedTask;
    }

    public Task SetDoneAsync(TodoItem item, bool isDone)
    {
        if (FailWrites) throw new IOException("Simulated storage failure");
        Items.Single(t => t.Id == item.Id).IsDone = isDone;
        item.IsDone = isDone;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(TodoItem item)
    {
        if (FailWrites) throw new IOException("Simulated storage failure");
        Items.RemoveAll(t => t.Id == item.Id);
        return Task.CompletedTask;
    }

    private static TodoItem Clone(TodoItem t) => new()
    {
        Id = t.Id, Title = t.Title, Notes = t.Notes, Priority = t.Priority,
        DueDate = t.DueDate, IsDone = t.IsDone, CategoryId = t.CategoryId, CategoryName = t.CategoryName
    };
}
