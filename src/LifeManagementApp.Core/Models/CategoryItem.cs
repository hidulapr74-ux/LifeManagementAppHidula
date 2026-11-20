namespace LifeManagementApp.Core.Models;

public class CategoryItem
{
    public int Id { get; init; }
    public string Name { get; init; } = "";
    public override string ToString() => Name;
}
