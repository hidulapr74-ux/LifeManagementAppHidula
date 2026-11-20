// AI-assisted tests; see README.md (AI use).
using LifeManagementApp.Core.Models;
using LifeManagementApp.Core.ViewModels;
using LifeManagementApp.Tests.Fakes;

namespace LifeManagementApp.Tests;

public class TodoListViewModelTests
{
    [Fact]
    public async Task Filters_CombineCategoryStatusAndCaseInsensitiveSearch()
    {
        var repo = new FakeTodoRepository();
        repo.Items.AddRange(new[]
        {
            new TodoItem { Id = 1, Title = "Read C#", CategoryId = 1 },
            new TodoItem { Id = 2, Title = "Read email", CategoryId = 2 },
            new TodoItem { Id = 3, Title = "Read old notes", CategoryId = 1, IsDone = true }
        });
        var vm = new TodoListViewModel(repo, new FakeNavigationService(), new FakeDialogService());
        await vm.LoadCommand.ExecuteAsync(null);
        Assert.Equal(3, vm.Todos.Count); // The default is ALL tasks.
        vm.SelectedCategory = vm.Categories.Single(c => c.Id == 1);
        vm.SelectedStatus = "Open";
        vm.SearchText = "READ";
        Assert.Equal(1, Assert.Single(vm.Todos).Id);
    }

    [Fact]
    public async Task TwoSortOrders_ProduceDifferentExpectedOrders()
    {
        var repo = new FakeTodoRepository();
        repo.Items.AddRange(new[]
        {
            new TodoItem { Id = 1, Priority = 5, DueDate = new DateTime(2026, 11, 25) },
            new TodoItem { Id = 2, Priority = 1, DueDate = new DateTime(2026, 11, 21) }
        });
        var vm = new TodoListViewModel(repo, new FakeNavigationService(), new FakeDialogService());
        await vm.LoadCommand.ExecuteAsync(null);
        Assert.Equal(new[] { 1, 2 }, vm.Todos.Select(t => t.Id));
        vm.SelectedSort = "Due date";
        Assert.Equal(new[] { 2, 1 }, vm.Todos.Select(t => t.Id));
    }

    [Fact]
    public async Task FailedCompletionWrite_DoesNotPretendTheTaskIsDone()
    {
        var repo = new FakeTodoRepository();
        repo.Items.Add(new TodoItem { Id = 1, Title = "Study" });
        var vm = new TodoListViewModel(repo, new FakeNavigationService(), new FakeDialogService());
        await vm.LoadCommand.ExecuteAsync(null);
        repo.FailWrites = true;
        await vm.ToggleDoneCommand.ExecuteAsync(vm.Todos[0]);
        Assert.False(vm.Todos[0].IsDone);
        Assert.True(vm.HasError);
    }

    [Fact]
    public async Task DeleteCancelled_KeepsTheTask()
    {
        var repo = new FakeTodoRepository();
        repo.Items.Add(new TodoItem { Id = 1, Title = "Keep me" });
        var vm = new TodoListViewModel(repo, new FakeNavigationService(), new FakeDialogService { Confirm = false });
        await vm.LoadCommand.ExecuteAsync(null);
        await vm.DeleteCommand.ExecuteAsync(vm.Todos[0]);
        Assert.Single(repo.Items);
        Assert.Single(vm.Todos);
    }

    [Fact]
    public async Task Edit_PassesTheSelectedTaskIdToNavigation()
    {
        var navigation = new FakeNavigationService();
        var vm = new TodoListViewModel(new FakeTodoRepository(), navigation, new FakeDialogService());
        await vm.EditCommand.ExecuteAsync(new TodoItem { Id = 42 });
        Assert.Equal(42, navigation.LastEditorId);
    }
}
