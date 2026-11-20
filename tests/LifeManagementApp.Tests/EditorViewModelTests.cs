// AI-assisted tests; see README.md (AI use).
using LifeManagementApp.Core.Models;
using LifeManagementApp.Core.ViewModels;
using LifeManagementApp.Tests.Fakes;

namespace LifeManagementApp.Tests;

public class EditorViewModelTests
{
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("12345678901234567890123456789012345678901")]
    public async Task InvalidTitles_CannotBeSaved(string title)
    {
        var repo = new FakeTodoRepository();
        var vm = new AddTodoViewModel(repo, new FakeNavigationService());
        await vm.LoadAsync(null);
        vm.Title = title;
        Assert.False(vm.SaveCommand.CanExecute(null));
        await vm.SaveCommand.ExecuteAsync(null);
        Assert.Empty(repo.Items);
    }

    [Fact]
    public async Task Create_TrimsTheTitleAndNavigatesBackAfterSaving()
    {
        var repo = new FakeTodoRepository();
        var nav = new FakeNavigationService();
        var vm = new AddTodoViewModel(repo, nav);
        await vm.LoadAsync(null);
        vm.Title = "  Prepare presentation  ";
        vm.Priority = 5;
        Assert.True(vm.SaveCommand.CanExecute(null));
        await vm.SaveCommand.ExecuteAsync(null);
        var item = Assert.Single(repo.Items);
        Assert.Equal("Prepare presentation", item.Title);
        Assert.Equal(5, item.Priority);
        Assert.True(item.Id > 0);
        Assert.Equal(1, nav.BackCalls);
    }

    [Fact]
    public async Task Editing_PreservesTheIdAndCompletionStatus()
    {
        var repo = new FakeTodoRepository();
        repo.Items.Add(new TodoItem { Id = 7, Title = "Old", IsDone = true, CategoryId = 1 });
        var vm = new AddTodoViewModel(repo, new FakeNavigationService());
        await vm.LoadAsync(7);
        vm.Title = "Updated";
        await vm.SaveCommand.ExecuteAsync(null);
        var item = Assert.Single(repo.Items);
        Assert.Equal(7, item.Id);
        Assert.Equal("Updated", item.Title);
        Assert.True(item.IsDone);
    }

    [Fact]
    public async Task FailedSave_KeepsTheFormAndDoesNotNavigateAway()
    {
        var repo = new FakeTodoRepository { FailWrites = true };
        var nav = new FakeNavigationService();
        var vm = new AddTodoViewModel(repo, nav);
        await vm.LoadAsync(null);
        vm.Title = "Keep my text";
        await vm.SaveCommand.ExecuteAsync(null);
        Assert.True(vm.HasError);
        Assert.Equal("Keep my text", vm.Title);
        Assert.Equal(0, nav.BackCalls);
        Assert.False(vm.IsBusy);
    }

    [Fact]
    public async Task FailedNavigationAfterSaving_DoesNotCreateADuplicateOnRetry()
    {
        var repo = new FakeTodoRepository();
        var nav = new FakeNavigationService { FailGoingBack = true };
        var vm = new AddTodoViewModel(repo, nav);
        await vm.LoadAsync(null);
        vm.Title = "Just once";
        await vm.SaveCommand.ExecuteAsync(null);
        nav.FailGoingBack = false;
        await vm.SaveCommand.ExecuteAsync(null);
        Assert.Single(repo.Items);
        Assert.Equal(2, repo.SaveCalls);
    }

    [Fact]
    public async Task ChangingTitle_NotifiesTheBindingAndRechecksSave()
    {
        var vm = new AddTodoViewModel(new FakeTodoRepository(), new FakeNavigationService());
        await vm.LoadAsync(null);
        var changed = new List<string?>();
        int rechecks = 0;
        vm.PropertyChanged += (_, e) => changed.Add(e.PropertyName);
        vm.SaveCommand.CanExecuteChanged += (_, _) => rechecks++;
        vm.Title = "Study";
        Assert.Contains(nameof(vm.TitleCounter), changed);
        Assert.True(rechecks > 0);
        Assert.Equal("5/40", vm.TitleCounter);
    }
}
