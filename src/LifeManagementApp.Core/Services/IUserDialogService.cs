// AI-assisted code for the course project; see README.md (AI use).
namespace LifeManagementApp.Core.Services;

public interface IUserDialogService
{
    Task<bool> ConfirmDeleteAsync(string title);
}
