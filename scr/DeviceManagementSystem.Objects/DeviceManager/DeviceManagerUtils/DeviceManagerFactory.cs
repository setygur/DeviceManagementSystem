namespace DeviceManagementSystem.Objects.DeviceManager.DeviceManagerUtils;

public static class DeviceManagerFactory
{
    /// <summary>
    /// DeviceManagerFactory
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns>
    /// new instance of DeviceManager
    /// </returns>
    public static IDeviceManager CreateDeviceManager(string filePath)
    {
        return new DeviceManager(filePath);
    }
}