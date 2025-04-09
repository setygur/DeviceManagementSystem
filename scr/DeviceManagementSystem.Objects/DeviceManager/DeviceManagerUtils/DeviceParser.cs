using DeviceManagementSystem.Objects.Devices;

namespace DeviceManagementSystem.Objects.DeviceManager.DeviceManagerUtils;

public static class DeviceParser
{
    /// <summary>
    /// Takes a string with device data and parse it into new object Device
    /// </summary>
    /// <param name="deviceData"></param>
    /// <returns>
    /// new Device object of a specific type
    /// null if deviceData is incorrect
    /// </returns>
    public static Device? ParseDevice(string deviceData)
    {
        String[] splitDeviceData = deviceData.Split(',');
        if (splitDeviceData[0].StartsWith("SW"))
        {
            try
            {
                if (splitDeviceData.Length == 4)
                {
                    var id = splitDeviceData[0];
                    var name = splitDeviceData[1];
                    var isOn = bool.Parse(splitDeviceData[2]);
                    var batteryPercentage = int.Parse(splitDeviceData[3].Substring(0,
                        splitDeviceData[3].Length - 1));

                    return new Smartwatch(id, name, isOn, batteryPercentage);
                }
            }
            catch(ArgumentException){}
            return null;
            
        }
        
        if (splitDeviceData[0].StartsWith("P"))
        {

            try
            {
                if (splitDeviceData.Length == 4)
                {
                    var id = splitDeviceData[0];
                    var name = splitDeviceData[1];
                    var isOn = bool.Parse(splitDeviceData[2]);
                    var system = splitDeviceData[3];
                    if (system.Equals("null"))
                    {
                        system = null;
                    }

                    return new PersonalComputer(id, name, isOn, system);
                }
            }
            catch(ArgumentException){}

            Console.WriteLine("Invalid device data: " + deviceData);
            return null;
            
        }
        
        if (splitDeviceData[0].StartsWith("ED"))
        {
            try
            {
                if (splitDeviceData.Length == 5)
                {
                    var id = splitDeviceData[0];
                    var name = splitDeviceData[1];
                    var isOn = bool.Parse(splitDeviceData[2]);
                    var ip = splitDeviceData[3];
                    var network = splitDeviceData[4];

                    return new EmbeddedDevice(id, name, isOn, ip, network);
                }
            }
            catch(ArgumentException){}

            Console.WriteLine("Invalid device data: " + deviceData);
            return null;
            
        }
        Console.WriteLine("Invalid device data: " + deviceData);
        return null;
    }
}