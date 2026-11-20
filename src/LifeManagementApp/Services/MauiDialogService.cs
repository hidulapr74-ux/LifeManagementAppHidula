// AI-assisted code for the course project; see README.md (AI use).
using LifeManagementApp.Core.Services;

namespace LifeManagementApp.Services;

public class MauiDialogService : IUserDialogService
{
    public Task<bool> ConfirmDeleteAsync(string title)
        => Shell.Current.DisplayAlert("Delete task?", $"Delete \"{title}\"? This removes it from your list.", "Delete", "Keep task");
}
