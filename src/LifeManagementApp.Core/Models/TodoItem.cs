using CommunityToolkit.Mvvm.ComponentModel;

namespace LifeManagementApp.Core.Models;

// AI-assisted implementation; see the README's AI use section.
public partial class TodoItem : ObservableObject
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Notes { get; set; } = "";
    public int Priority { get; set; } = 3;
    public DateTime DueDate { get; set; } = DateTime.Today.AddDays(1);
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = "";

    [ObservableProperty]
    private bool isDone;
}
