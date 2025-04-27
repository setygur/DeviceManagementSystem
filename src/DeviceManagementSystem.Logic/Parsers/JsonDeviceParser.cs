using System.Text.Json;
using System.Text.Json.Nodes;
using DeviceManagementSystem.Logic.Parsers.ParsersExceptions;
using DeviceManagementSystem.Objects.Devices;

namespace DeviceManagementSystem.Logic.Parsers;

public class JsonDeviceParser : IDeviceParser
{
    private static readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task<Device?> ParseAsync(Stream input)
    {
        using var reader = new StreamReader(input);
        var jsonText = await reader.ReadToEndAsync();
        var json = JsonNode.Parse(jsonText);
        if (json == null) return null;

        Device? device = json switch
        {
            JsonObject obj when obj.ContainsKey("batterypercentage")
                => JsonSerializer.Deserialize<Smartwatch>(json.ToJsonString(), _options),

            JsonObject obj when obj.ContainsKey("operatingsystem")
                => JsonSerializer.Deserialize<PersonalComputer>(json.ToJsonString(), _options),

            JsonObject obj when obj.ContainsKey("ip") && obj.ContainsKey("networkname")
                => JsonSerializer.Deserialize<EmbeddedDevice>(json.ToJsonString(), _options),

            _ => null
        };

        if (device == null)
            return null;

        try
        {
            device.Validate();
        }
        catch (Exception e)
        {
            throw new ValidationException(e.Message);
        }

        return device;
    }
}