// AI-assisted implementation; see the README's AI use section.
namespace LifeManagementApp.Data.Entities;

// A plain database row. No UI controls or toolkit dependency belong here.
public class TodoEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Notes { get; set; } = "";
    public int Priority { get; set; } = 3;
    public DateTime DueDate { get; set; } = DateTime.Today.AddDays(1);
    public bool IsDone { get; set; }
    public int CategoryId { get; set; }
    public CategoryEntity? Category { get; set; }
}
