// AI-assisted tests; see README.md (AI use).
using LifeManagementApp.Core.Models;
using LifeManagementApp.Core.ViewModels;
using LifeManagementApp.Tests.Fakes;

namespace LifeManagementApp.Tests;

public class HomeViewModelTests
{
    [Fact]
    public async Task OfflineWeather_DoesNotPreventLocalTasksFromLoading()
    {
        var repo = new FakeTodoRepository();
        repo.Items.Add(new TodoItem { Id = 1, Title = "Prepare demo", Priority = 5, DueDate = DateTime.Today });
        var weather = new FakeWeatherService { Failure = new HttpRequestException("Offline") };
        var vm = new HomeViewModel(repo, weather, new FakeNavigationService());
        await vm.LoadCommand.ExecuteAsync(null);
        Assert.Single(vm.TopTodos);
        Assert.Equal(1, vm.OpenCount);
        Assert.Equal(1, vm.DueTodayCount);
        Assert.True(vm.HasWeatherError);
        Assert.False(vm.HasWeather);
        Assert.False(vm.IsBusy);
        Assert.False(vm.IsWeatherLoading);
    }

    [Fact]
    public async Task RefreshAfterRecovery_ClearsTheErrorAndShowsCurrentWeather()
    {
        var weather = new FakeWeatherService { Failure = new HttpRequestException("Offline") };
        var vm = new HomeViewModel(new FakeTodoRepository(), weather, new FakeNavigationService());
        await vm.LoadCommand.ExecuteAsync(null);
        weather.Failure = null;
        await vm.RefreshWeatherCommand.ExecuteAsync(null);
        Assert.True(vm.HasWeather);
        Assert.False(vm.HasWeatherError);
        Assert.Equal("Kitesurf weather!", vm.KiteHeadline);
    }

    [Fact]
    public async Task ReturningToHome_ReloadsCompletionAndCounts()
    {
        var repo = new FakeTodoRepository();
        repo.Items.Add(new TodoItem { Id = 1, Title = "Study", Priority = 5 });
        var vm = new HomeViewModel(repo, new FakeWeatherService(), new FakeNavigationService());
        await vm.LoadCommand.ExecuteAsync(null);
        repo.Items[0].IsDone = true;
        await vm.LoadCommand.ExecuteAsync(null);
        Assert.Empty(vm.TopTodos);
        Assert.Equal(0, vm.OpenCount);
        Assert.Equal(1, vm.DoneCount);
    }
}
