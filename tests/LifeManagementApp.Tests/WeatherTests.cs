// AI-assisted tests; see README.md (AI use).
using System.Net;
using System.Text;
using System.Text.Json;
using LifeManagementApp.Core.Rules;
using LifeManagementApp.Core.Services;
using LifeManagementApp.Tests.Fakes;

namespace LifeManagementApp.Tests;

public class WeatherTests
{
    private const string ValidJson = """
        {"current_units":{"temperature_2m":"°C","wind_speed_10m":"m/s"},"current":{"temperature_2m":7.5,"wind_speed_10m":5.1}}
        """;

    [Theory]
    [InlineData(0, false)]
    [InlineData(4.99, false)]
    [InlineData(5, false)]
    [InlineData(5.01, true)]
    [InlineData(20, true)]
    [InlineData(-1, false)]
    [InlineData(double.NaN, false)]
    [InlineData(double.PositiveInfinity, false)]
    public void KiteAlert_UsesAStrictFiveMetresPerSecondLimit(double wind, bool expected)
        => Assert.Equal(expected, WeatherRules.IsKitesurfWeather(wind));

    [Fact]
    public async Task Api_MapsJson_AndRequestsMetresPerSecond()
    {
        using var handler = Handler(ValidJson);
        using var http = new HttpClient(handler);
        var weather = await new OpenMeteoWeatherService(http).GetCurrentAsync();
        Assert.Equal(7.5, weather.TemperatureCelsius);
        Assert.Equal(5.1, weather.WindSpeedMetersPerSecond);
        Assert.True(weather.IsKitesurfWeather);
        Assert.Contains("wind_speed_unit=ms", handler.LastUri!.Query);
        Assert.Contains("latitude=63.10", handler.LastUri.Query);
    }

    [Theory]
    [InlineData("{}")]
    [InlineData("{\"current\":{\"temperature_2m\":4}}")]
    [InlineData("not JSON")]
    public async Task Api_RejectsMissingOrMalformedReadings(string json)
    {
        using var handler = Handler(json);
        using var http = new HttpClient(handler);
        await Assert.ThrowsAsync<JsonException>(() => new OpenMeteoWeatherService(http).GetCurrentAsync());
    }

    [Fact]
    public async Task Api_RejectsKilometresPerHourInsteadOfMisreadingTheKiteLimit()
    {
        using var handler = Handler(ValidJson.Replace("m/s", "km/h"));
        using var http = new HttpClient(handler);
        await Assert.ThrowsAsync<JsonException>(() => new OpenMeteoWeatherService(http).GetCurrentAsync());
    }

    [Fact]
    public async Task Api_NonSuccessStatusIsAnError()
    {
        using var handler = new StubHttpHandler { Respond = _ => new HttpResponseMessage(HttpStatusCode.ServiceUnavailable) };
        using var http = new HttpClient(handler);
        await Assert.ThrowsAsync<HttpRequestException>(() => new OpenMeteoWeatherService(http).GetCurrentAsync());
    }

    private static StubHttpHandler Handler(string json) => new()
    {
        Respond = _ => new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(json, Encoding.UTF8, "application/json") }
    };
}
