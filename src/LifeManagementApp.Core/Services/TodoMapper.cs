// AI-assisted code for the course project; see README.md (AI use).
using LifeManagementApp.Core.Models;
using LifeManagementApp.Data.Entities;

namespace LifeManagementApp.Core.Services;

public static class TodoMapper
{
    public static TodoItem ToItem(TodoEntity entity) => new()
    {
        Id = entity.Id, Title = entity.Title, Notes = entity.Notes,
        Priority = entity.Priority, DueDate = entity.DueDate, IsDone = entity.IsDone,
        CategoryId = entity.CategoryId, CategoryName = entity.Category?.Name ?? ""
    };

    public static TodoEntity ToEntity(TodoItem item) => new()
    {
        Id = item.Id, Title = item.Title, Notes = item.Notes,
        Priority = item.Priority, DueDate = item.DueDate, IsDone = item.IsDone,
        CategoryId = item.CategoryId
    };
}
