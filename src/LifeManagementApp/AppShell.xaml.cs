// AI-assisted code for the course project; see README.md (AI use).
using LifeManagementApp.Views;

namespace LifeManagementApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(AddTodoPage), typeof(AddTodoPage));
    }
}
