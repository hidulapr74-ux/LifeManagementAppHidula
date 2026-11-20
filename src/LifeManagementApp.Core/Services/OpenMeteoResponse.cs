using System.Text.Json.Serialization;

namespace LifeManagementApp.Core.Services;

public class OpenMeteoResponse
{
    [JsonPropertyName("current")]
    public OpenMeteoCurrent? Current { get; set; }

    [JsonPropertyName("current_units")]
    public OpenMeteoUnits? Units { get; set; }
}
