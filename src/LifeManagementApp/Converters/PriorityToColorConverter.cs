// AI-assisted code for the course project; see README.md (AI use).
using System.Globalization;

namespace LifeManagementApp.Converters;

public class PriorityToColorConverter : IValueConverter
{
    // Colours belong in the UI layer, not in TodoItem or a ViewModel.
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is int priority && priority >= 4 ? Color.FromArgb("#A35C18") : Color.FromArgb("#146C67");

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
