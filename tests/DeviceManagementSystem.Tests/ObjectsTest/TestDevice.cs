using DeviceManagementSystem.Objects.Devices;

namespace DeviceManagementSystem.Tests.ObjectsTest;

public class TestDevice : Device
{
    public TestDevice(string id, string name, bool isOn) : base(id, name, isOn) { }

    public override void Validate() { }
}