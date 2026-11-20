using LifeManagementApp.Core.Models;
using LifeManagementApp.Core.Services;

namespace LifeManagementApp.Tests.Fakes;

public class FakeWeatherService : IWeatherService
{
    public Exception? Failure { get; set; }
    public Task<WeatherInfo> GetCurrentAsync(CancellationToken cancellationToken = default)
    {
        if (Failure is not null) throw Failure;
        return Task.FromResult(new WeatherInfo { TemperatureCelsius = 8, WindSpeedMetersPerSecond = 5.1, RetrievedAt = DateTimeOffset.Now });
    }
}
