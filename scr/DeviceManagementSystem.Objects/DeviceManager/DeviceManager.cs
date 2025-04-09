using DeviceManagementSystem.Objects.DeviceManager.DeviceManagerUtils;
using DeviceManagementSystem.Objects.Devices;

namespace DeviceManagementSystem.Objects.DeviceManager;

public class DeviceManager : IDeviceManager
{
    private DeviceRepository _repository;

    /// <summary>
    /// A Constructor for a new DeviceManager object, filePath is used to import data from file
    /// </summary>
    /// <param name="filePath"></param>
    public DeviceManager(string filePath)
    {
        _repository = new DeviceRepository();
        ImportDevices(filePath);
    }
    
    /// <summary>
    /// Reads the data from file filePath and one by one, parse it and add it to the _repository
    /// </summary>
    /// <param name="filePath"></param>
    public void ImportDevices(string filePath)
    {
        var deviceData = DeviceFileService.GetDevicesFromFile(filePath);
        foreach (var data in deviceData)
        {
            var device = DeviceParser.ParseDevice(data);
            if (device != null) _repository.AddDevice(device);
        }
    }

    /// <summary>
    /// Read the file filePath, parse its content into Device and returns the collection
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns>
    /// creates new Device?[]
    /// </returns>
    public Device?[] GetDevicesFromFile(string filePath) //cannot remove due to tests
    { 
        Device?[] devices = new Device[15];
        var deviceData = DeviceFileService.GetDevicesFromFile(filePath);
        foreach (var data in deviceData)
        {
            var device = DeviceParser.ParseDevice(data);
            for (int i = 0; i < devices.Length; i++)
            {
                if (devices[i] == null)
                {
                    devices[i] = device;
                }
            }
        }
        return devices;
    }
    
    /// <summary>
    /// Adds a Device to _repository
    /// </summary>
    /// <param name="device"></param>
    public void AddDevice(Device device) =>
        _repository.AddDevice(device);

    public Device? GetById(string id) => _repository.GetById(id);
    /// <summary>
    /// Removes a Device from _repository
    /// </summary>
    /// <param name="device"></param>
    public void RemoveDevice(Device device) =>
        _repository.RemoveDevice(device);
    
    /// <summary>
    /// Removes a device by Id from _repository
    /// </summary>
    /// <param name="id"></param>
    public void RemoveDeviceById(string id) => _repository.RemoveDeviceById(id);

    /// <summary>
    /// Edits Device in _repository using matching Ids
    /// </summary>
    /// <param name="editDevice"></param>
    public void EditDevice(Device editDevice) =>
        _repository.EditDevice(editDevice);
    
    /// <summary>
    /// Replace the device with a given Id with new Device
    /// </summary>
    /// <param name="id"></param>
    /// <param name="editDevice"></param>
    public void EditDeviceById(string id, Device editDevice) =>
        _repository.EditDeviceById(id, editDevice);

    /// <summary>
    /// Turns a device on
    /// </summary>
    /// <param name="device"></param>
    public void TurnOnDevice(Device device) =>
        device.turnOn();

    /// <summary>
    /// Turns a Device off
    /// </summary>
    /// <param name="device"></param>
    public void TurnOffDevice(Device device) =>
        device.turnOff();

    /// <summary>
    /// Outputs the contents of _repository to console
    /// </summary>
    public void ShowAllDevices() =>
        _repository.ShowAllDevices();

    /// <summary>
    /// Export a _repository content to a file outputPath
    /// </summary>
    /// <param name="outputPath"></param>
    public void ExportDevices(String outputPath) =>
        DeviceFileService.ExportDevices(outputPath, _repository.GetDevices());

    /// <summary>
    /// Returns content of _repository
    /// </summary>
    /// <returns>Device?[]</returns>
    public Device?[] GetDevices() =>
        _repository.GetDevices();
}