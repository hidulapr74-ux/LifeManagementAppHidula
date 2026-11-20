// AI-assisted code for the course project; see README.md (AI use).
using System.Text.Json.Serialization;

namespace LifeManagementApp.Core.Services;

public class OpenMeteoCurrent
{
    [JsonPropertyName("temperature_2m")]
    public double? Temperature { get; set; }

    [JsonPropertyName("wind_speed_10m")]
    public double? WindSpeed { get; set; }
}
