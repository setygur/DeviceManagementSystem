using DeviceManagementSystem.Objects.Devices;
using DeviceManagementSystem.Objects.Devices.DevicesExceptions;

namespace DeviceManagementSystem.Tests.ObjectsTest;

public class SmartwatchTest
{
    [Fact]
    public void Constructor_ShouldInitializeCorrectly()
    {
        var smartwatch = new Smartwatch("1", "TestWatch", false, 50);
        Assert.NotNull(smartwatch);
        Assert.Equal(50, smartwatch.BatteryPercentage);
    }

    [Fact]
    public void BatteryPercentage_ShouldTriggerPowerLow_WhenBelow20()
    {
        var smartwatch = new Smartwatch("1", "TestWatch", false, 50);
        smartwatch.BatteryPercentage = 15;
        Assert.Equal(15, smartwatch.BatteryPercentage);
    }

    [Fact]
    public void BatteryPercentage_ShouldNotChange_WhenOutOfRange()
    {
        var smartwatch = new Smartwatch("1", "TestWatch", false, 50);
        smartwatch.BatteryPercentage = 150;
        Assert.Equal(50, smartwatch.BatteryPercentage);
    }

    [Fact]
    public void TurnOn_ShouldThrowEmptyBatteryException_WhenBatteryBelow11()
    {
        var smartwatch = new Smartwatch("1", "TestWatch", false, 10);
        Assert.Throws<EmptyBatteryException>(() => smartwatch.turnOn());
    }

    [Fact]
    public void TurnOn_ShouldReturnTrue_WhenBatteryAbove11()
    {
        var smartwatch = new Smartwatch("1", "TestWatch", false, 50);
        var result = smartwatch.turnOn();
        Assert.True(result);
    }

    [Fact]
    public void ToString_ShouldReturnCorrectFormat()
    {
        var smartwatch = new Smartwatch("1", "TestWatch", false, 50);
        var result = smartwatch.ToString();
        Assert.Equal("SW-1,TestWatch,False,50", result);
    }
}