// AI-assisted code for the course project; see README.md (AI use).
using System.Globalization;

namespace LifeManagementApp.Converters;

public class DoneActionConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is true ? "Reopen" : "Done";

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
