using System.Text.Json.Serialization;

namespace DeviceManagementSystem.Objects.Devices;
using DeviceManagementSystem.Objects.Devices.DevicesExceptions;

public class PersonalComputer : Device
{
    public string? OperatingSystem { get; private set; }
    
    /// <summary>
    /// A constructor for a PersonalComputer object, take OperatingSystem as a param
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="isOn"></param>
    /// <param name="operatingSystem"></param>
    [JsonConstructor]
    public PersonalComputer(string id, string name, bool isOn, string? operatingSystem) : base(id, name, isOn)
    {
        OperatingSystem = operatingSystem;
    }

    /// <summary>
    /// Turns on a device if it has OperatingSystem
    /// Returns true if successful
    /// </summary>
    /// <returns></returns>
    /// <exception cref="EmptySystemException"></exception>
    public override bool turnOn()
    {
        if (string.IsNullOrWhiteSpace(OperatingSystem))
        {
            return base.turnOn();
        }
        throw new EmptySystemException();
    }

    /// <summary>
    /// Return data device
    /// </summary>
    /// <returns>string</returns>
    public override string ToString()
    {
        return Id + "," + Name + "," + IsOn + "," + OperatingSystem;
    }

    public override void Validate()
    {
        if (IsOn && OperatingSystem == null)
        {
            throw new EmptySystemException();
        }
    }
}