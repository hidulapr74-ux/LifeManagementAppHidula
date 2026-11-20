// AI-assisted code for the course project; see README.md (AI use).
using LifeManagementApp.Core.Models;

namespace LifeManagementApp.Core.Services;

public interface ITodoRepository
{
    Task<List<TodoItem>> GetAllAsync();
    Task<TodoItem?> GetByIdAsync(int id);
    Task<List<CategoryItem>> GetCategoriesAsync();
    Task SaveAsync(TodoItem item);
    Task SetDoneAsync(TodoItem item, bool isDone);
    Task DeleteAsync(TodoItem item);
}
