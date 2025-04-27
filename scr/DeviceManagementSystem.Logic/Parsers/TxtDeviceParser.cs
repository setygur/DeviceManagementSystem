using DeviceManagementSystem.Objects.Devices;

namespace DeviceManagementSystem.Logic.Parsers;

public class TxtDeviceParser : IDeviceParser
{

    public async Task<Device?> ParseAsync(Stream input)
    {
        using var reader = new StreamReader(input);
        var line = await reader.ReadToEndAsync();

        return ParseDevice(line);
    }

    private Device? ParseDevice(string deviceData)
    {
        var splitDeviceData = deviceData.Split(',');
        if (splitDeviceData[0].StartsWith("SW") && splitDeviceData.Length == 4)
        {
            return new Smartwatch(
                splitDeviceData[0],
                splitDeviceData[1],
                bool.Parse(splitDeviceData[2]),
                int.Parse(splitDeviceData[3].TrimEnd('%')));
        }

        if (splitDeviceData[0].StartsWith("P") && splitDeviceData.Length == 4)
        {
            var system = splitDeviceData[3] == "null" ? null : splitDeviceData[3];
            return new PersonalComputer(
                splitDeviceData[0],
                splitDeviceData[1],
                bool.Parse(splitDeviceData[2]),
                system);
        }

        if (splitDeviceData[0].StartsWith("ED") && splitDeviceData.Length == 5)
        {
            return new EmbeddedDevice(
                splitDeviceData[0],
                splitDeviceData[1],
                bool.Parse(splitDeviceData[2]),
                splitDeviceData[3],
                splitDeviceData[4]);
        }
        return null;
    }
}