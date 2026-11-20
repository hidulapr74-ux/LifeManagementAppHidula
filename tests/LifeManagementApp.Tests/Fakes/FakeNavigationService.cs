using LifeManagementApp.Core.Services;

namespace LifeManagementApp.Tests.Fakes;

public class FakeNavigationService : INavigationService
{
    public int? LastEditorId { get; private set; }
    public int BackCalls { get; private set; }
    public bool FailGoingBack { get; set; }

    public Task OpenTodoEditorAsync(int? todoId = null) { LastEditorId = todoId; return Task.CompletedTask; }
    public Task OpenTodosAsync() => Task.CompletedTask;
    public Task GoBackAsync()
    {
        if (FailGoingBack) throw new InvalidOperationException("Simulated navigation failure");
        BackCalls++;
        return Task.CompletedTask;
    }
}
