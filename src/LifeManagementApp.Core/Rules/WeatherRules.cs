namespace LifeManagementApp.Core.Rules;

public static class WeatherRules
{
    public const double KiteWindLimit = 5.0;

    // Strictly greater: a wind speed of exactly 5.0 m/s does NOT trigger the alert.
    public static bool IsKitesurfWeather(double windSpeed)
        => double.IsFinite(windSpeed) && windSpeed > KiteWindLimit;
}
