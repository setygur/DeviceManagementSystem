namespace DeviceManagementSystem.Objects.Devices;

public abstract class Device
{
    public string Id { get; set; }
    public string Name { get; set; }
    public bool IsOn { get; set; }

    /// <summary>
    /// A Constructor for a Device object
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="isOn"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public Device(string id, string name, bool isOn)
    {
        if (id != null)
        {
            Id = id;
        }
        else
        {
            throw new ArgumentNullException(nameof(id));
        }

        if (name != null)
        {
            Name = name;
        }
        else
        {
            throw new ArgumentNullException(nameof(name));
        }
        IsOn = isOn;
    }

    /// <summary>
    /// Turns on the device if it is not turned on already, return true if successful
    /// </summary>
    /// <returns>
    /// true if operation is successful, false otherwise
    /// </returns>
    public virtual bool turnOn()
    {
        if (!IsOn)
        {
            IsOn = true;
            return true;
        }
        Console.WriteLine("Device is already on");
        return false;
    }

    /// <summary>
    /// Turns off the device if it is not turned off already, return true if successful
    /// </summary>
    /// <returns>
    ///true if operation is successful, false otherwise
    /// </returns>
    public virtual bool turnOff()
    {
        if (IsOn)
        {
            IsOn = false;
            return true;
        }
        Console.WriteLine("Device is already off");
        return false;
    }

    /// <summary>
    /// Virtual method to return the Device data
    /// </summary>
    /// <returns>
    /// Device data in string
    /// </returns>
    public override string ToString() //cannot change to abstract due to tests
    {
        return "Unknown Device";
    }
    
    public abstract void Validate();
}