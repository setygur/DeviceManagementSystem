using System.Text.Json.Serialization;
using DeviceManagementSystem.Objects.Devices.DevicesExceptions;

namespace DeviceManagementSystem.Objects.Devices;

public class Smartwatch : Device, IPowerNotifier
{
    private int _batteryPercentage;
    /// <summary>
    /// A constructor for a Smartwatch object
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="isOn"></param>
    /// <param name="batteryPercentage"></param>
    [JsonConstructor]
    public Smartwatch(string id, string name, bool isOn, int batteryPercentage) : base(id, name, isOn)
    {
        BatteryPercentage = batteryPercentage; 
    }

    /// <summary>
    /// Getter and setter for _batteryPercentage, ensures the value is in range 0,100
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public int BatteryPercentage
    {
        get { return _batteryPercentage; }
        set
        {
            if (value >= 0 || value <= 100)
            {
                _batteryPercentage = value;
                if (_batteryPercentage < 20)
                {
                    PowerLow();
                }
            }
            else
            {
                throw new ArgumentException("Battery percentage must be between 0 and 100.");
            }
        }
    }
    
    /// <summary>
    /// Notifies a user via console whenever power is low
    /// </summary>
    public void PowerLow()
    {
        Console.WriteLine("PowerLow");
    }

    /// <summary>
    /// Turns a device on if battery is enough, otherwise throws exception
    /// </summary>
    /// <returns></returns>
    /// <exception cref="EmptyBatteryException"></exception>
    public override bool turnOn()
    {
        if (_batteryPercentage < 11)
        {
            throw new EmptyBatteryException();
        }

        return base.turnOn();
    }

    /// <summary>
    /// Return data device
    /// </summary>
    /// <returns>string</returns>
    public override string ToString()
    {
        return Id + "," + Name + "," + IsOn + "," + _batteryPercentage;
    }

    public override void Validate()
    {
        if (_batteryPercentage > 100 || _batteryPercentage < 0)
        {
            throw new ArgumentException("Battery percentage must be between 0 and 100.");
        }
    }
}