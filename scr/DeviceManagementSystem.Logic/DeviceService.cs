using DeviceManagementSystem.Objects.Devices;
using DeviceManagementSystem.Database.DBMock;

namespace DeviceManagementSystem.Logic;

public static class DeviceService
{
    /// <summary>
    /// Returns the list of devices in db
    /// </summary>
    /// <returns>List of all Devices from database manager</returns>
    public static IEnumerable<Device?> GetAll() => StaticDB.Manager.GetDevices();

    /// <summary>
    /// Returns device by id from db
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Return Device with matching id</returns>
    public static Device? GetById(string id) => StaticDB.Manager.GetById(id);
    
    /// <summary>
    /// Creates new EmbeddedDevice in db
    /// </summary>
    /// <param name="device"></param>
    public static void CreateEmbeddedDevice(EmbeddedDevice device) => StaticDB.Manager.AddDevice(device);
    /// <summary>
    /// Creates new PersonalComputer in db
    /// </summary>
    /// <param name="device"></param>
    public static void CreatePersonalComputer(PersonalComputer device) => StaticDB.Manager.AddDevice(device);
    /// <summary>
    /// Creates a new Smartwatch in db
    /// </summary>
    /// <param name="device"></param>
    public static void CreateSmartwatch(Smartwatch device) => StaticDB.Manager.AddDevice(device);

    /// <summary>
    /// Replaces a device with matching id with provided EmbeddedDevice
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updatedDevice"></param>
    public static void UpdateEmbeddedDevice(string id, EmbeddedDevice updatedDevice) =>
        StaticDB.Manager.EditDeviceById(id, updatedDevice);
    /// <summary>
    /// Replaces a device with matching id with provided PersonalComputer
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updatedDevice"></param>
    public static void UpdatePersonalComputer(string id, PersonalComputer updatedDevice) =>
        StaticDB.Manager.EditDeviceById(id, updatedDevice);
    /// <summary>
    /// Replaces a device with matching id with provided Smartwatch
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updatedDevice"></param>
    public static void UpdateSmartwatch(string id, Smartwatch updatedDevice) =>
        StaticDB.Manager.EditDeviceById(id, updatedDevice);

    /// <summary>
    /// Removes Device with matching Id
    /// </summary>
    /// <param name="id"></param>
    public static void Delete(string id) => StaticDB.Manager.RemoveDeviceById(id);
}