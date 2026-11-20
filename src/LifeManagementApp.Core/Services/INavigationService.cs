// AI-assisted code for the course project; see README.md (AI use).
namespace LifeManagementApp.Core.Services;

// The ViewModels ask for navigation; only the MAUI implementation knows Shell.
public interface INavigationService
{
    Task OpenTodoEditorAsync(int? todoId = null);
    Task OpenTodosAsync();
    Task GoBackAsync();
}
