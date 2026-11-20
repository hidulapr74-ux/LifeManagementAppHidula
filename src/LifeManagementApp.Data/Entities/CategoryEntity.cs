// AI-assisted implementation; see the README's AI use section.
namespace LifeManagementApp.Data.Entities;

public class CategoryEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public ICollection<TodoEntity> Todos { get; set; } = new List<TodoEntity>();
}
