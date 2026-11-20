// AI-assisted code for the course project; see README.md (AI use).
using LifeManagementApp.Core.Models;

namespace LifeManagementApp.Core.Services;

public interface IWeatherService
{
    Task<WeatherInfo> GetCurrentAsync(CancellationToken cancellationToken = default);
}
