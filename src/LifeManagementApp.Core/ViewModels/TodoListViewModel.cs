using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LifeManagementApp.Core.Models;
using LifeManagementApp.Core.Rules;
using LifeManagementApp.Core.Services;

namespace LifeManagementApp.Core.ViewModels;

// AI-assisted implementation; see the README's AI use section.
public partial class TodoListViewModel : BaseViewModel
{
    private readonly ITodoRepository repository;
    private readonly INavigationService navigation;
    private readonly IUserDialogService dialogs;
    private List<TodoItem> allTodos = new();

    public TodoListViewModel(ITodoRepository repository, INavigationService navigation, IUserDialogService dialogs)
    {
        this.repository = repository;
        this.navigation = navigation;
        this.dialogs = dialogs;
    }

    public ObservableCollection<TodoItem> Todos { get; } = new();
    public ObservableCollection<CategoryItem> Categories { get; } = new();
    public IReadOnlyList<string> SortOptions { get; } = new[] { "Priority", "Due date" };
    public IReadOnlyList<string> StatusOptions { get; } = new[] { "All tasks", "Open", "Completed" };

    [ObservableProperty] private string selectedSort = "Priority";
    [ObservableProperty] private string selectedStatus = "All tasks";
    [ObservableProperty] private string searchText = "";
    [ObservableProperty] private CategoryItem? selectedCategory;
    [ObservableProperty] private string countLabel = "No tasks yet";
    [ObservableProperty] private string feedback = "";

    partial void OnSelectedSortChanged(string value) => ApplyFilters();
    partial void OnSelectedStatusChanged(string value) => ApplyFilters();
    partial void OnSearchTextChanged(string value) => ApplyFilters();
    partial void OnSelectedCategoryChanged(CategoryItem? value) => ApplyFilters();

    [RelayCommand]
    private async Task LoadAsync()
    {
        if (IsBusy) return;
        IsBusy = true;
        ErrorMessage = "";
        try
        {
            int categoryId = SelectedCategory?.Id ?? 0;
            List<CategoryItem> categories = await repository.GetCategoriesAsync();
            allTodos = await repository.GetAllAsync();
            Categories.Clear();
            Categories.Add(new CategoryItem { Id = 0, Name = "All categories" });
            foreach (CategoryItem category in categories) Categories.Add(category);
            SelectedCategory = Categories.FirstOrDefault(c => c.Id == categoryId) ?? Categories[0];
            ApplyFilters();
        }
        catch (Exception ex) { ShowError(ex, "We couldn't load your tasks. Try Refresh."); }
        finally { IsBusy = false; }
    }

    public void ApplyFilters()
    {
        IEnumerable<TodoItem> query = allTodos;
        if (SelectedCategory is { Id: > 0 }) query = query.Where(t => t.CategoryId == SelectedCategory.Id);
        if (SelectedStatus == "Open") query = query.Where(t => !t.IsDone);
        if (SelectedStatus == "Completed") query = query.Where(t => t.IsDone);
        if (!string.IsNullOrWhiteSpace(SearchText))
            query = query.Where(t => t.Title.Contains(SearchText.Trim(), StringComparison.OrdinalIgnoreCase));

        TodoSort order = SelectedSort == "Due date" ? TodoSort.DueDate : TodoSort.Priority;
        List<TodoItem> visible = TodoRules.Sort(query, order).ToList();
        Todos.Clear();
        foreach (TodoItem item in visible) Todos.Add(item);
        CountLabel = $"{visible.Count} of {allTodos.Count} tasks · {allTodos.Count(t => !t.IsDone)} open";
    }

    [RelayCommand]
    private async Task ToggleDoneAsync(TodoItem item)
    {
        if (IsBusy) return;
        IsBusy = true;
        ErrorMessage = "";
        try
        {
            await repository.SetDoneAsync(item, !item.IsDone);
            Feedback = item.IsDone ? "Task completed. Nicely done." : "Task reopened.";
            ApplyFilters();
        }
        catch (Exception ex) { ShowError(ex, "We couldn't update this task. Try again."); }
        finally { IsBusy = false; }
    }

    [RelayCommand]
    private async Task DeleteAsync(TodoItem item)
    {
        if (IsBusy) return;
        IsBusy = true;
        ErrorMessage = "";
        try
        {
            if (!await dialogs.ConfirmDeleteAsync(item.Title)) return;
            await repository.DeleteAsync(item);
            allTodos.Remove(item);
            ApplyFilters();
            Feedback = "Task deleted.";
        }
        catch (Exception ex) { ShowError(ex, "We couldn't delete this task. Try again."); }
        finally { IsBusy = false; }
    }

    [RelayCommand]
    private async Task AddTodoAsync()
    {
        try { await navigation.OpenTodoEditorAsync(); }
        catch (Exception ex) { ShowError(ex, "We couldn't open the task editor. Try again."); }
    }

    [RelayCommand]
    private async Task EditAsync(TodoItem item)
    {
        try { await navigation.OpenTodoEditorAsync(item.Id); }
        catch (Exception ex) { ShowError(ex, "We couldn't open this task. Try again."); }
    }
}
