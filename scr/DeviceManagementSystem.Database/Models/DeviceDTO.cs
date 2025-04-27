using System.Text.Json.Serialization;

namespace DeviceManagementSystem.Database.Models;

public class DeviceDTO
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public bool? IsOn { get; set; }

    [JsonConstructor]
    public DeviceDTO(string? id, string? name, bool? isOn)
    {
        Id = id;
        Name = name;
        IsOn = isOn;
    }
}