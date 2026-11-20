using System.Net.Http.Json;
using System.Text.Json;
using LifeManagementApp.Core.Models;

namespace LifeManagementApp.Core.Services;

// AI-assisted implementation; see the README's AI use section.
public class OpenMeteoWeatherService : IWeatherService
{
    private readonly HttpClient httpClient;
    public const string RequestUrl = "https://api.open-meteo.com/v1/forecast?latitude=63.10&longitude=21.62&current=temperature_2m,wind_speed_10m&wind_speed_unit=ms&temperature_unit=celsius&timezone=auto";

    public OpenMeteoWeatherService(HttpClient httpClient) { this.httpClient = httpClient; }

    public async Task<WeatherInfo> GetCurrentAsync(CancellationToken cancellationToken = default)
    {
        OpenMeteoResponse? response = await httpClient.GetFromJsonAsync<OpenMeteoResponse>(RequestUrl, cancellationToken);
        if (response?.Current?.Temperature is not double temperature ||
            response.Current.WindSpeed is not double wind ||
            !double.IsFinite(temperature) || !double.IsFinite(wind) || wind < 0)
            throw new JsonException("The weather response is missing valid readings.");
        if (response.Units?.WindSpeed != "m/s" || response.Units.Temperature != "°C")
            throw new JsonException("The weather response uses unexpected units.");

        return new WeatherInfo
        {
            TemperatureCelsius = temperature,
            WindSpeedMetersPerSecond = wind,
            RetrievedAt = DateTimeOffset.Now
        };
    }
}
