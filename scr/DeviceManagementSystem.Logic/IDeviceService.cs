using DeviceManagementSystem.Objects.Devices;

namespace DeviceManagementSystem.Logic;

public interface IDeviceService
{
    IEnumerable<Device?> GetAll();

    Device? GetById(string id);

    bool CreateDevice(Device device);

    bool UpdateDevice(string id, Device updatedDevice);

    bool Delete(string id);

}