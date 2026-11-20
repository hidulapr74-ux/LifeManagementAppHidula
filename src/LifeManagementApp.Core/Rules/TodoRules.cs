using LifeManagementApp.Core.Models;

namespace LifeManagementApp.Core.Rules;

// AI-assisted implementation; see the README's AI use section.
public static class TodoRules
{
    public static List<TodoItem> GetTopThree(IEnumerable<TodoItem> items)
        => Sort(items.Where(t => !t.IsDone), TodoSort.Priority).Take(3).ToList();

    public static IOrderedEnumerable<TodoItem> Sort(IEnumerable<TodoItem> items, TodoSort sort)
        => sort == TodoSort.DueDate
            ? items.OrderBy(t => t.DueDate).ThenByDescending(t => t.Priority).ThenBy(t => t.Id)
            : items.OrderByDescending(t => t.Priority).ThenBy(t => t.DueDate).ThenBy(t => t.Id);
}
