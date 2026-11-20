// AI-assisted code for the course project; see README.md (AI use).
using LifeManagementApp.Core.ViewModels;

namespace LifeManagementApp.Views;

public partial class AddTodoPage : ContentPage, IQueryAttributable
{
    private readonly AddTodoViewModel viewModel;
    private int? todoId;

    public AddTodoPage(AddTodoViewModel viewModel)
    {
        InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = viewModel;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        todoId = query.TryGetValue("TodoId", out object? value) && int.TryParse(value?.ToString(), out int id)
            ? id : null;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.LoadAsync(todoId);
    }
}
