using DeviceManagementSystem.Objects.Devices;

namespace DeviceManagementSystem.Objects.DeviceManager;

public interface IDeviceManager
{
    void ImportDevices(string filePath);

    Device?[] GetDevicesFromFile(string filePath);

    void AddDevice(Device device);

    Device? GetById(string id);

    void RemoveDevice(Device device);

    void RemoveDeviceById(string id);

    void EditDevice(Device editDevice);

    void EditDeviceById(string id, Device editDevice);

    void TurnOnDevice(Device device);

    void TurnOffDevice(Device device);

    void ShowAllDevices();

    void ExportDevices(string outputPath);

    Device?[] GetDevices();
}