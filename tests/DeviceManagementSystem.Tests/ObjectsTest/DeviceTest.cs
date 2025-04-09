namespace DeviceManagementSystem.Tests.ObjectsTest;

public class DeviceTest
{
    [Fact]
    public void TurnOn_ShouldReturnTrue_WhenDeviceIsOff()
    {
        TestDevice device = new TestDevice("1", "TestDevice", false);
        
        var result = device.turnOn();

        Assert.True(result);
    }

    [Fact]
    public void TurnOn_ShouldReturnFalse_WhenDeviceIsAlreadyOn()
    {
        TestDevice device = new TestDevice("1", "TestDevice", true);

        var result = device.turnOn();

        Assert.False(result);
    }

    [Fact]
    public void TurnOff_ShouldReturnTrue_WhenDeviceIsOn()
    {
        TestDevice device = new TestDevice("1", "TestDevice", true);

        var result = device.turnOff();

        Assert.True(result);
    }

    [Fact]
    public void TurnOff_ShouldReturnFalse_WhenDeviceIsAlreadyOff()
    {
        TestDevice device = new TestDevice("1", "TestDevice", false);

        var result = device.turnOff();

        Assert.False(result);
    }

    [Fact]
    public void ToString_ShouldReturnUnknownDevice()
    {
        TestDevice device = new TestDevice("1", "TestDevice", false);

        var result = device.ToString();

        Assert.Equal("Unknown Device", result);
    }
}