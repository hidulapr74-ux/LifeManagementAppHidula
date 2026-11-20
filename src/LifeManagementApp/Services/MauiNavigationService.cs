// AI-assisted code for the course project; see README.md (AI use).
using LifeManagementApp.Core.Services;
using LifeManagementApp.Views;

namespace LifeManagementApp.Services;

public class MauiNavigationService : INavigationService
{
    public Task OpenTodoEditorAsync(int? todoId = null)
        => todoId.HasValue
            ? Shell.Current.GoToAsync(nameof(AddTodoPage), new Dictionary<string, object> { ["TodoId"] = todoId.Value })
            : Shell.Current.GoToAsync(nameof(AddTodoPage));

    public Task OpenTodosAsync() => Shell.Current.GoToAsync("//main/tasks");
    public Task GoBackAsync() => Shell.Current.GoToAsync("..");
}
