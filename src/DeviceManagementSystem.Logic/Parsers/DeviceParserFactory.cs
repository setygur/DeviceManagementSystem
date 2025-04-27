namespace DeviceManagementSystem.Logic.Parsers;

public class DeviceParserFactory
{
    public static IDeviceParser? GetParser(string? contentType)
    {
        return contentType?.ToLower() switch
        {
            "application/json" => new JsonDeviceParser(),
            "text/plain" => new TxtDeviceParser(),
            _ => null
        };
    }
}