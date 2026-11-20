using LifeManagementApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LifeManagementApp.Data;

// AI-assisted implementation; see the README's AI use section.
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<TodoEntity> Todos => Set<TodoEntity>();
    public DbSet<CategoryEntity> Categories => Set<CategoryEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryEntity>(category =>
        {
            category.ToTable("Categories");
            category.Property(c => c.Name).IsRequired().HasMaxLength(40);
            category.HasIndex(c => c.Name).IsUnique();
            category.HasData(
                new CategoryEntity { Id = 1, Name = "Study" },
                new CategoryEntity { Id = 2, Name = "Work" },
                new CategoryEntity { Id = 3, Name = "Home" },
                new CategoryEntity { Id = 4, Name = "Personal" });
        });
        modelBuilder.Entity<TodoEntity>(todo =>
        {
            todo.ToTable("Todos", table =>
            {
                table.HasCheckConstraint("CK_Todos_Priority", "Priority BETWEEN 1 AND 5");
                table.HasCheckConstraint("CK_Todos_Title", "length(trim(Title)) BETWEEN 1 AND 40");
            });
            todo.Property(t => t.Title).IsRequired().HasMaxLength(40);
            todo.Property(t => t.Notes).HasMaxLength(1000);
            todo.HasOne(t => t.Category).WithMany(c => c.Todos)
                .HasForeignKey(t => t.CategoryId).OnDelete(DeleteBehavior.Restrict);
        });
    }
}
