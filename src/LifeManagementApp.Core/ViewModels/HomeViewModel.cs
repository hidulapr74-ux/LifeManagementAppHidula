using System.Collections.ObjectModel;
using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LifeManagementApp.Core.Models;
using LifeManagementApp.Core.Rules;
using LifeManagementApp.Core.Services;

namespace LifeManagementApp.Core.ViewModels;

// AI-assisted implementation; see the README's AI use section.
public partial class HomeViewModel : BaseViewModel
{
    private readonly ITodoRepository repository;
    private readonly IWeatherService weatherService;
    private readonly INavigationService navigation;

    public HomeViewModel(ITodoRepository repository, IWeatherService weatherService, INavigationService navigation)
    {
        this.repository = repository;
        this.weatherService = weatherService;
        this.navigation = navigation;
    }

    public ObservableCollection<TodoItem> TopTodos { get; } = new();
    public string TodayLabel => DateTime.Today.ToString("dddd, d MMMM", CultureInfo.GetCultureInfo("en-GB"));

    [ObservableProperty] private int openCount;
    [ObservableProperty] private int doneCount;
    [ObservableProperty] private int dueTodayCount;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasWeather))]
    [NotifyPropertyChangedFor(nameof(KiteHeadline))]
    [NotifyPropertyChangedFor(nameof(KiteDescription))]
    private WeatherInfo? weather;

    [ObservableProperty] private bool isWeatherLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasWeatherError))]
    private string weatherError = "";

    public bool HasWeather => Weather is not null;
    public bool HasWeatherError => WeatherError.Length > 0;
    public string KiteHeadline => Weather?.IsKitesurfWeather == true ? "Kitesurf weather!" : "Calmer winds today";
    public string KiteDescription => Weather?.IsKitesurfWeather == true
        ? "The wind is above 5 m/s. Your kite alert is on."
        : "The kite alert starts when wind is over 5 m/s.";

    [RelayCommand]
    private async Task LoadAsync()
    {
        if (IsBusy) return;
        IsBusy = true;
        ErrorMessage = "";
        OnPropertyChanged(nameof(TodayLabel));
        try { await Task.WhenAll(LoadTodosAsync(), RefreshWeatherAsync()); }
        finally { IsBusy = false; }
    }

    private async Task LoadTodosAsync()
    {
        try
        {
            List<TodoItem> items = await repository.GetAllAsync();
            OpenCount = items.Count(t => !t.IsDone);
            DoneCount = items.Count(t => t.IsDone);
            DueTodayCount = items.Count(t => !t.IsDone && t.DueDate.Date == DateTime.Today);
            TopTodos.Clear();
            foreach (TodoItem item in TodoRules.GetTopThree(items)) TopTodos.Add(item);
        }
        catch (Exception ex) { ShowError(ex, "We couldn't load your tasks. Try Refresh."); }
    }

    [RelayCommand]
    private async Task RefreshWeatherAsync()
    {
        if (IsWeatherLoading) return;
        IsWeatherLoading = true;
        WeatherError = "";
        try { Weather = await weatherService.GetCurrentAsync(); }
        catch (Exception ex)
        {
            Weather = null; // Do not label an old forecast as current after a failure.
            System.Diagnostics.Debug.WriteLine(ex);
            WeatherError = "Weather is unavailable. Check your connection and try again.";
        }
        finally { IsWeatherLoading = false; }
    }

    [RelayCommand]
    private async Task AddTodoAsync()
    {
        try { await navigation.OpenTodoEditorAsync(); }
        catch (Exception ex) { ShowError(ex, "We couldn't open the task editor. Try again."); }
    }

    [RelayCommand]
    private async Task OpenTodosAsync()
    {
        try { await navigation.OpenTodosAsync(); }
        catch (Exception ex) { ShowError(ex, "We couldn't open your tasks. Try again."); }
    }
}
