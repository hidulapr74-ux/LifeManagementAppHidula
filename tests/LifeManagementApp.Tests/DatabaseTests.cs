using LifeManagementApp.Core.Models;
using LifeManagementApp.Core.Services;
using LifeManagementApp.Data;
using LifeManagementApp.Tests.Fakes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LifeManagementApp.Tests;

public class DatabaseTests
{
    [Fact]
    public async Task FirstUse_CreatesDatabaseFromMigrationsAndSeedsFourCategories()
    {
        using var fixture = new DatabaseTestFixture();
        var categories = await fixture.Repository.GetCategoriesAsync();
        Assert.Equal(4, categories.Count);
        var factory = fixture.Provider.GetRequiredService<IDbContextFactory<AppDbContext>>();
        await using var db = await factory.CreateDbContextAsync();
        Assert.Single(await db.Database.GetAppliedMigrationsAsync());
        Assert.False(db.Database.HasPendingModelChanges());
    }

    [Fact]
    public async Task SavedTask_SurvivesANewServiceProviderAndKeepsItsCategoryRelation()
    {
        using var fixture = new DatabaseTestFixture();
        var task = new TodoItem { Title = "SQLite persists", CategoryId = 1, Priority = 5 };
        await fixture.Repository.SaveAsync(task);
        Assert.True(task.Id > 0);
        using var reopened = fixture.OpenAnotherProvider();
        var stored = Assert.Single(await reopened.GetRequiredService<ITodoRepository>().GetAllAsync());
        Assert.Equal("SQLite persists", stored.Title);
        Assert.Equal("Study", stored.CategoryName);
        Assert.Equal(task.Id, stored.Id);
    }

    [Fact]
    public async Task EditCompleteAndDelete_ArePersistedByRealSQLite()
    {
        using var fixture = new DatabaseTestFixture();
        var task = new TodoItem { Title = "Original", CategoryId = 1 };
        await fixture.Repository.SaveAsync(task);
        task.Title = "Edited";
        task.CategoryId = 2;
        await fixture.Repository.SaveAsync(task);
        await fixture.Repository.SetDoneAsync(task, true);
        var stored = await fixture.Repository.GetByIdAsync(task.Id);
        Assert.NotNull(stored);
        Assert.Equal("Edited", stored.Title);
        Assert.Equal("Work", stored.CategoryName);
        Assert.True(stored.IsDone);
        await fixture.Repository.DeleteAsync(task);
        Assert.Empty(await fixture.Repository.GetAllAsync());
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task EmptyTitle_IsRejectedEvenWhenTheFormIsBypassed(string title)
    {
        using var fixture = new DatabaseTestFixture();
        await Assert.ThrowsAsync<ArgumentException>(() => fixture.Repository.SaveAsync(new TodoItem { Title = title, CategoryId = 1 }));
    }

    [Fact]
    public async Task MissingCategory_IsRejected()
    {
        using var fixture = new DatabaseTestFixture();
        await Assert.ThrowsAsync<ArgumentException>(() => fixture.Repository.SaveAsync(new TodoItem { Title = "Bad relation", CategoryId = 999 }));
    }

    [Fact]
    public async Task ConcurrentFirstReads_UseOneSafeDatabaseInitialization()
    {
        using var fixture = new DatabaseTestFixture();
        await Task.WhenAll(fixture.Repository.GetAllAsync(), fixture.Repository.GetCategoriesAsync());
        Assert.Equal(4, (await fixture.Repository.GetCategoriesAsync()).Count);
    }

    [Fact]
    public async Task UsedCategory_CannotBeDeletedByTheDatabase()
    {
        using var fixture = new DatabaseTestFixture();
        await fixture.Repository.SaveAsync(new TodoItem { Title = "Linked task", CategoryId = 1 });
        var factory = fixture.Provider.GetRequiredService<IDbContextFactory<AppDbContext>>();
        await using var db = await factory.CreateDbContextAsync();
        db.Categories.Remove((await db.Categories.FindAsync(1))!);
        await Assert.ThrowsAsync<DbUpdateException>(() => db.SaveChangesAsync());
    }
}
