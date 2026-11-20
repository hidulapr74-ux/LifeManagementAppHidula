// AI-assisted tests; see README.md (AI use).
using LifeManagementApp.Core.Services;

namespace LifeManagementApp.Tests.Fakes;

public class FakeDialogService : IUserDialogService
{
    public bool Confirm { get; set; }
    public Task<bool> ConfirmDeleteAsync(string title) => Task.FromResult(Confirm);
}
