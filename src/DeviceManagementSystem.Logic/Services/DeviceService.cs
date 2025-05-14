using DeviceManagementSystem.Database.Models;
using DeviceManagementSystem.Database.Repositories;
using DeviceManagementSystem.Objects.Devices;

namespace DeviceManagementSystem.Logic.Services;

public class DeviceService : IDeviceService
{
    private string _connectionString;
    private IRepository _repository;
    public DeviceService(string connectionString)
    {
        _connectionString = connectionString;
        _repository = new MSSQLRepository(connectionString);
    }
    
    public IEnumerable<DeviceDTO> GetAllDevices() => _repository.GetAllDevices();
    
    /// <summary>
    /// Returns device by id from db
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Return Device with matching id</returns>
    public Device? GetDeviceById(string id) => _repository.GetDeviceById(id);

    public bool CreateDevice(Device device) => _repository.CreateDevice(device);

    public bool UpdateDevice(Device updatedDevice) => _repository.UpdateDevice(updatedDevice);

    public bool DeleteDeviceById(string id) => _repository.DeleteDeviceById(id);
}