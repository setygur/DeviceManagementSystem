using DeviceManagementSystem.Database.Models;
using DeviceManagementSystem.Objects.Devices;

namespace DeviceManagementSystem.Database.Repositories;

public interface IRepository
{
    IEnumerable<DeviceDTO> GetAllDevices();

    Device? GetDeviceById(string id);

    bool CreateDevice(Device device);

    bool UpdateDevice(Device updatedDevice);

    public bool DeleteDeviceById(string id);
}