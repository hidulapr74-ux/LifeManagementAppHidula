// AI-assisted code for the course project; see README.md (AI use).
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace LifeManagementApp.Core.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool isBusy;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasError))]
    private string errorMessage = "";

    public bool IsNotBusy => !IsBusy;
    public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);

    protected void ShowError(Exception exception, string message)
    {
        Debug.WriteLine(exception);
        ErrorMessage = message;
    }
}
