namespace LifeManagementApp.Core.Services;

public interface IUserDialogService
{
    Task<bool> ConfirmDeleteAsync(string title);
}
