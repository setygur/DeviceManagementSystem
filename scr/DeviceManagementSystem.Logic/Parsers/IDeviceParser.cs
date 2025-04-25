using DeviceManagementSystem.Objects.Devices;

namespace DeviceManagementSystem.Logic.Parsers;

public interface IDeviceParser
{
    Task<Device?> ParseAsync(Stream input);
}