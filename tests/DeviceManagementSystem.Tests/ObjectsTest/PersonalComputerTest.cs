using DeviceManagementSystem.Objects.Devices;
using DeviceManagementSystem.Objects.Devices.DevicesExceptions;

namespace DeviceManagementSystem.Tests.ObjectsTest;

public class PersonalComputerTest
{
    [Fact]
    public void Constructor_ShouldInitializeCorrectly_WithoutOperatingSystem()
    {
        var pc = new PersonalComputer("1", "TestPC", false, "Windows");
        Assert.NotNull(pc);
    }

    [Fact]
    public void Constructor_ShouldInitializeCorrectly_WithOperatingSystem()
    {
        var pc = new PersonalComputer("1", "TestPC", false, "Windows 10");
        Assert.NotNull(pc);
    }

    [Fact]
    public void TurnOn_ShouldThrowEmptySystemException_WhenOperatingSystemIsNull()
    {
        var pc = new PersonalComputer("1", "TestPC", false, "Windows");
        Assert.Throws<EmptySystemException>(() => pc.turnOn());
    }

    [Fact]
    public void TurnOn_ShouldReturnTrue_WhenOperatingSystemIsNotNull()
    {
        var pc = new PersonalComputer("1", "TestPC", false, "Windows 10");
        var result = pc.turnOn();
        Assert.True(result);
    }

    [Fact]
    public void ToString_ShouldReturnCorrectFormat()
    {
        var pc = new PersonalComputer("1", "TestPC", false, "Windows 10");
        var result = pc.ToString();
        Assert.Equal("P-1,TestPC,False,Windows 10", result);
    }
}