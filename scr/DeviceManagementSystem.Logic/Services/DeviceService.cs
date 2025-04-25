using DeviceManagementSystem.Database.DBMock;
using DeviceManagementSystem.Objects.Devices;

namespace DeviceManagementSystem.Logic.Services;

public class DeviceService
{
    private string _connectionString;
    
    public DeviceService(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    /// <summary>
    /// Returns the list of devices in db
    /// </summary>
    /// <returns>List of all Devices from database manager</returns>
    public IEnumerable<Device?> GetAll() => StaticDB.Manager.GetDevices(); //TODO return short information

    /// <summary>
    /// Returns device by id from db
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Return Device with matching id</returns>
    public Device? GetById(string id) => StaticDB.Manager.GetById(id); //TODO return all information
    
    public bool CreateDevice(Device device) => throw new NotImplementedException();

    public bool UpdateDevice(string id, Device updatedDevice) => throw new NotImplementedException();

    public bool Delete(string id) => throw new NotImplementedException();
}