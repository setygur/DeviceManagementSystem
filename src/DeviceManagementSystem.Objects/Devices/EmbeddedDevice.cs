using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using DeviceManagementSystem.Objects.Devices.DevicesExceptions;

namespace DeviceManagementSystem.Objects.Devices;

public class EmbeddedDevice : Device
{
    private string _ip;
    public string NetworkName { get; set; }
    public byte[]? RowVersion { get; set; }

    /// <summary>
    /// A Constructor for a EmbeddedDevice object
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="isOn"></param>
    /// <param name="ip"></param>
    /// <param name="networkName"></param>
    /// <exception cref="ArgumentNullException"></exception>
    [JsonConstructor]
    public EmbeddedDevice(string id, string name, bool isOn, string ip, string networkName) 
        : base(id, name, isOn)
    {
        if (networkName != null)
        {
            NetworkName = networkName;
        }
        else
        {
            throw new ArgumentNullException(nameof(networkName));
        }
        Ip = ip;

        if (isOn = true && Connect(networkName))
        {
            throw new ConnectionException();
        }
    }
    public EmbeddedDevice(string id, string name, bool isOn, string ip, string networkName, byte[]? rowVersion) 
        : base(id, name, isOn)
    {
        if (networkName != null)
        {
            NetworkName = networkName;
        }
        else
        {
            throw new ArgumentNullException(nameof(networkName));
        }
        Ip = ip;

        if (isOn = true && Connect(networkName))
        {
            throw new ConnectionException();
        }
        RowVersion = rowVersion;
    }
    
    /// <summary>
    /// Provide a regexCheck to Ip before setting it as a private field
    /// Returns _ip
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public string? Ip
    {
        get => _ip;
        set
        {
            if (CheckIp(value))
            {
                _ip = value;
            }
            else
            {
                throw new ArgumentException($"Invalid IP address: {value}");
            }
        }
    }

    /// <summary>
    /// Checks ip for a IPv4 regex
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    private static bool CheckIp(string ip)
    {
        if (Regex.IsMatch(ip,
                "^((25[0-5]|2[0-4][0-9]|[0-1]?[0-9][0-9]?)\\.){3}(25[0-5]|2[0-4][0-9]|[0-1]?[0-9][0-9]?)$"))
        {
            return true;
        }

        throw new ArgumentException();
    }

    /// <summary>
    /// Checks if the network name contains a proper string
    /// </summary>
    /// <param name="networkName"></param>
    /// <returns>
    /// true if operation is successful, false otherwise
    /// </returns>
    /// <exception cref="ConnectionException"></exception>
    private bool Connect(String networkName)
    {
        if (networkName.Contains("MD Ltd."))
        {
            return true;
        }

        throw new ConnectionException();
    }

    /// <summary>
    /// Turns on a Device if the network name contains a proper string
    /// Returns true if operation successful
    /// </summary>
    /// <returns>
    /// true if operation is successful, false otherwise
    /// </returns>
    public override bool turnOn()
    {
        if (Connect(NetworkName))
        {
            return base.turnOn();
        }

        return false;
    }

    /// <summary>
    /// Return data device
    /// </summary>
    /// <returns>
    /// Device data in string
    /// </returns>
    public override string ToString()
    {
        return Id + "," + Name + "," + _ip + "," + NetworkName; 
    }

    public override void Validate()
    {
        if (!CheckIp(_ip))
        {
            throw new ArgumentException("Invalid IP address");
        }
        if (IsOn && Connect(NetworkName))
        {
            throw new ConnectionException();
        }
    }
}