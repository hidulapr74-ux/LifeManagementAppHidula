// AI-assisted code for the course project; see README.md (AI use).
using LifeManagementApp.Core.ViewModels;

namespace LifeManagementApp.Views;

public partial class TodoListPage : ContentPage
{
    private readonly TodoListViewModel viewModel;
    public TodoListPage(TodoListViewModel viewModel)
    {
        InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.LoadCommand.ExecuteAsync(null);
    }
}
