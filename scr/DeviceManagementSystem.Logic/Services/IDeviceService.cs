using DeviceManagementSystem.Database.Models;
using DeviceManagementSystem.Objects.Devices;

namespace DeviceManagementSystem.Logic.Services;

public interface IDeviceService
{
    IEnumerable<DeviceDTO> GetAllDevices();

    Device? GetDeviceById(string id);

    bool CreateDevice(Device device);

    bool UpdateDevice(Device updatedDevice);

    public bool DeleteDeviceById(string id);

}