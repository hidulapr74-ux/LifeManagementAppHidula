using System.Text.Json.Serialization;

namespace LifeManagementApp.Core.Services;

public class OpenMeteoUnits
{
    [JsonPropertyName("temperature_2m")]
    public string? Temperature { get; set; }

    [JsonPropertyName("wind_speed_10m")]
    public string? WindSpeed { get; set; }
}
