using DeviceManagementSystem.Objects.Devices;

namespace DeviceManagementSystem.Objects.DeviceManager.DeviceManagerUtils;

public class DeviceRepository
{
    /// <summary>
    /// Private array to store Devices
    /// </summary>
    private Device?[] _devices = new Device?[15];

    /// <summary>
    /// Add device to _devices if there is empty space
    /// </summary>
    /// <param name="device"></param>
    public void AddDevice(Device device)
    {
        for (int i = 0; i < _devices.Length; i++)
        {
            if (_devices[i] == null)
            {
                _devices[i] = device;
                Console.WriteLine("Device inserted at index " + i);
                return;
            }
        }
        Console.WriteLine("No empty space available to insert the device.");
    }
    
    /// <summary>
    /// Method to get a Device by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Device with the right Id</returns>
    public Device? GetById(string id)
    {
        foreach (var device in _devices)
        {
            if (device != null)
            {
                if (device.Id.Equals(id))
                {
                    return device;
                }
            }
        }

        return null;
    }
    
    /// <summary>
    /// Look for the identical id in the devices and replace it with a new one
    /// </summary>
    /// <param name="editDevice"></param>
    /// <exception cref="ArgumentException"></exception>
    public void EditDevice(Device editDevice)
    {
        var targetDeviceIndex = -1;
        for (var index = 0; index < _devices.Length; index++)
        {
            var storedDevice = _devices[index];
            if (storedDevice != null)
            {
                if (storedDevice.Id.Equals(editDevice.Id))
                {
                    targetDeviceIndex = index;
                    break;
                }
            }
        }

        if (targetDeviceIndex == -1)
        {
            throw new ArgumentException($"Device with ID {editDevice.Id} is not stored.", nameof(editDevice));
        }

        if (editDevice is Smartwatch)
        {
            if (_devices[targetDeviceIndex] is Smartwatch)
            {
                _devices[targetDeviceIndex] = editDevice;
            }
            else
            {
                throw new ArgumentException($"Type mismatch between devices. " +
                                            $"Target device has type {_devices[targetDeviceIndex].GetType().Name}");
            }
        }
        
        if (editDevice is PersonalComputer)
        {
            if (_devices[targetDeviceIndex] is PersonalComputer)
            {
                _devices[targetDeviceIndex] = editDevice;
            }
            else
            {
                throw new ArgumentException($"Type mismatch between devices. " +
                                            $"Target device has type {_devices[targetDeviceIndex].GetType().Name}");
            }
        }
        
        if (editDevice is EmbeddedDevice)
        {
            if (_devices[targetDeviceIndex] is EmbeddedDevice)
            {
                _devices[targetDeviceIndex] = editDevice;
            }
            else
            {
                throw new ArgumentException($"Type mismatch between devices. " +
                                            $"Target device has type {_devices[targetDeviceIndex].GetType().Name}");
            }
        }
    }

    /// <summary>
    /// Look for id in the _devices and replaces a device with a new one
    /// </summary>
    /// <param name="id"></param>
    /// <param name="editDevice"></param>
    public void EditDeviceById(string id, Device editDevice)
    {
        var targetDeviceIndex = -1;
        for (var index = 0; index < _devices.Length; index++)
        {
            var storedDevice = _devices[index];
            if (storedDevice != null)
            {
                if (storedDevice.Id.Equals(editDevice.Id))
                {
                    targetDeviceIndex = index;
                    break;
                }
            }
        }

        if (targetDeviceIndex != -1)
        {
            _devices[targetDeviceIndex] = editDevice;
        }
    }
    
    /// <summary>
    /// Look for an Id match in the devices and removes it
    /// </summary>
    /// <param name="device"></param>
    public void RemoveDevice(Device device)
    {
        var deviceId = device.Id;
        for (int i = 0; i < _devices.Length; i++)
        {
            if (_devices[i] != null)
            {
                if (_devices[i].Id.Equals(deviceId))
                {
                    _devices[i] = null;
                    Console.WriteLine("Device removed at index " + i);
                    return;
                }
            }
        }
    }

    /// <summary>
    /// Removes a device by its Id field
    /// </summary>
    /// <param name="id"></param>
    public void RemoveDeviceById(string id)
    {
        for (int i = 0; i < _devices.Length; i++)
        {
            if (_devices[i] != null)
            {
                if (_devices[i].Id.Equals(id))
                {
                    _devices[i] = null;
                    return;
                }
            }
        }
    }

    /// <summary>
    /// Prints the current content of the Devices into console
    /// </summary>
    public void ShowAllDevices()
    {
        foreach (var x in _devices)
        {
            if (x != null)
            {
                Console.WriteLine(x.ToString());
            }
            else
            {
                Console.WriteLine("Empty device");
            }
        }
    }

    /// <summary>
    /// Returns Devices
    /// </summary>
    /// <returns>
    /// Device?[] content of a repository
    /// </returns>
    public Device?[] GetDevices() => _devices;
}