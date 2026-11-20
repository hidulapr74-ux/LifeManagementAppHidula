using LifeManagementApp.Core.Models;
using LifeManagementApp.Data;

namespace LifeManagementApp.Core.Services;

// AI-assisted implementation; see the README's AI use section.
public class TodoRepository : ITodoRepository
{
    private readonly TodoDataService data;
    public TodoRepository(TodoDataService data) { this.data = data; }

    public async Task<List<TodoItem>> GetAllAsync()
        => (await data.GetAllAsync()).Select(TodoMapper.ToItem).ToList();

    public async Task<TodoItem?> GetByIdAsync(int id)
    {
        var entity = await data.GetByIdAsync(id);
        return entity is null ? null : TodoMapper.ToItem(entity);
    }

    public async Task<List<CategoryItem>> GetCategoriesAsync()
        => (await data.GetCategoriesAsync())
            .Select(c => new CategoryItem { Id = c.Id, Name = c.Name }).ToList();

    public async Task SaveAsync(TodoItem item)
        => item.Id = await data.SaveAsync(TodoMapper.ToEntity(item));

    public async Task SetDoneAsync(TodoItem item, bool isDone)
    {
        await data.SetDoneAsync(item.Id, isDone);
        item.IsDone = isDone; // Change the screen only AFTER the database write succeeds.
    }

    public Task DeleteAsync(TodoItem item) => data.DeleteAsync(item.Id);
}
