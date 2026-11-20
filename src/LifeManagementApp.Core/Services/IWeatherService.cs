using LifeManagementApp.Core.Models;

namespace LifeManagementApp.Core.Services;

public interface IWeatherService
{
    Task<WeatherInfo> GetCurrentAsync(CancellationToken cancellationToken = default);
}
