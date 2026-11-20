using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LifeManagementApp.Core.Models;
using LifeManagementApp.Core.Services;

namespace LifeManagementApp.Core.ViewModels;

// AI-assisted implementation; see the README's AI use section.
public partial class AddTodoViewModel : BaseViewModel
{
    private readonly ITodoRepository repository;
    private readonly INavigationService navigation;
    private int? editingId;
    private bool savedIsDone;

    public AddTodoViewModel(ITodoRepository repository, INavigationService navigation)
    {
        this.repository = repository;
        this.navigation = navigation;
    }

    public ObservableCollection<CategoryItem> Categories { get; } = new();
    public IReadOnlyList<int> Priorities { get; } = new[] { 5, 4, 3, 2, 1 };
    public string PageTitle => editingId.HasValue ? "Edit task" : "A little more organised";
    public string SaveLabel => editingId.HasValue ? "Save changes" : "Create task";
    public string TitleCounter => $"{Title.Length}/40";

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    [NotifyPropertyChangedFor(nameof(TitleCounter))]
    private string title = "";

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private string notes = "";

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private int priority = 3;

    [ObservableProperty] private DateTime dueDate = DateTime.Today.AddDays(1);

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private CategoryItem? selectedCategory;

    public async Task LoadAsync(int? todoId)
    {
        if (IsBusy) return;
        IsBusy = true;
        ErrorMessage = "";
        editingId = todoId;
        OnPropertyChanged(nameof(PageTitle));
        OnPropertyChanged(nameof(SaveLabel));
        try
        {
            Categories.Clear();
            foreach (CategoryItem category in await repository.GetCategoriesAsync()) Categories.Add(category);
            if (todoId.HasValue)
            {
                TodoItem item = await repository.GetByIdAsync(todoId.Value)
                    ?? throw new KeyNotFoundException("The task no longer exists.");
                Title = item.Title;
                Notes = item.Notes;
                Priority = item.Priority;
                DueDate = item.DueDate;
                savedIsDone = item.IsDone;
                SelectedCategory = Categories.FirstOrDefault(c => c.Id == item.CategoryId);
            }
            else
            {
                Title = "";
                Notes = "";
                Priority = 3;
                DueDate = DateTime.Today.AddDays(1);
                savedIsDone = false;
                SelectedCategory = Categories.FirstOrDefault();
            }
        }
        catch (Exception ex) { ShowError(ex, "We couldn't open this task. Go back and try again."); }
        finally { IsBusy = false; }
    }

    private bool CanSave() => !string.IsNullOrWhiteSpace(Title) && Title.Length <= 40
        && Notes.Length <= 1000 && Priority is >= 1 and <= 5 && SelectedCategory is { Id: > 0 };

    [RelayCommand(CanExecute = nameof(CanSave))]
    private async Task SaveAsync()
    {
        if (IsBusy || !CanSave()) return;
        IsBusy = true;
        ErrorMessage = "";
        try
        {
            var item = new TodoItem
            {
                Id = editingId ?? 0, Title = Title.Trim(), Notes = Notes.Trim(),
                Priority = Priority, DueDate = DueDate.Date, IsDone = savedIsDone,
                CategoryId = SelectedCategory!.Id, CategoryName = SelectedCategory.Name
            };
            await repository.SaveAsync(item);
            editingId = item.Id; // A retry after a navigation failure updates, rather than duplicates.
            OnPropertyChanged(nameof(PageTitle));
            OnPropertyChanged(nameof(SaveLabel));
            await navigation.GoBackAsync();
        }
        catch (Exception ex) { ShowError(ex, "We couldn't finish saving. Your form is still here; try again."); }
        finally { IsBusy = false; }
    }

    [RelayCommand]
    private async Task CancelAsync()
    {
        if (IsBusy) return;
        try { await navigation.GoBackAsync(); }
        catch (Exception ex) { ShowError(ex, "We couldn't go back. Try again."); }
    }
}
