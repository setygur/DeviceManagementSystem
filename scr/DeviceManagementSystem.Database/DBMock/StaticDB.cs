using DeviceManagementSystem.Objects.DeviceManager;
using DeviceManagementSystem.Objects.DeviceManager.DeviceManagerUtils;

namespace DeviceManagementSystem.Database.DBMock;

public static class StaticDB
{
    //Stores a DeviceManager, unfortunately not IDeviceManager...
    public static DeviceManager Manager { get; } =
        DeviceManagerFactory.CreateDeviceManager(Path.Combine(AppContext.BaseDirectory, "DBMock", "database.txt"));
}