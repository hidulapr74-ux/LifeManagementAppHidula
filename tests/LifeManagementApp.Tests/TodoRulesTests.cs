using LifeManagementApp.Core.Models;
using LifeManagementApp.Core.Rules;

namespace LifeManagementApp.Tests;

public class TodoRulesTests
{
    [Fact]
    public void TopThree_ExcludesDone_AndBreaksPriorityTiesByDueDate()
    {
        DateTime day = new(2026, 11, 20);
        var tasks = new[]
        {
            new TodoItem { Id = 1, Priority = 3, DueDate = day },
            new TodoItem { Id = 2, Priority = 5, DueDate = day.AddDays(2) },
            new TodoItem { Id = 3, Priority = 5, DueDate = day.AddDays(1) },
            new TodoItem { Id = 4, Priority = 4, DueDate = day },
            new TodoItem { Id = 5, Priority = 5, DueDate = day, IsDone = true }
        };
        Assert.Equal(new[] { 3, 2, 4 }, TodoRules.GetTopThree(tasks).Select(t => t.Id));
    }

    [Fact]
    public void TopThree_HandlesAnEmptyList() => Assert.Empty(TodoRules.GetTopThree(Array.Empty<TodoItem>()));

    [Fact]
    public void TopThree_ReturnsOnlyAvailableOpenTasks()
    {
        var tasks = new[] { new TodoItem { Id = 1 }, new TodoItem { Id = 2, IsDone = true } };
        Assert.Equal(1, Assert.Single(TodoRules.GetTopThree(tasks)).Id);
    }

    [Fact]
    public void DateSort_PutsAnEarlierLowPriorityTaskBeforeALaterUrgentTask()
    {
        var tasks = new[]
        {
            new TodoItem { Id = 1, Priority = 5, DueDate = new DateTime(2026, 11, 25) },
            new TodoItem { Id = 2, Priority = 1, DueDate = new DateTime(2026, 11, 21) }
        };
        Assert.Equal(new[] { 2, 1 }, TodoRules.Sort(tasks, TodoSort.DueDate).Select(t => t.Id));
    }
}
