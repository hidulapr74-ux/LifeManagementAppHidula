// AI-assisted code for the course project; see README.md (AI use).
using LifeManagementApp.Core.Rules;

namespace LifeManagementApp.Core.Models;

public class WeatherInfo
{
    public double TemperatureCelsius { get; init; }
    public double WindSpeedMetersPerSecond { get; init; }
    public DateTimeOffset RetrievedAt { get; init; }
    public bool IsKitesurfWeather => WeatherRules.IsKitesurfWeather(WindSpeedMetersPerSecond);
}
