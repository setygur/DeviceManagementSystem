using DeviceManagementSystem.Objects.DeviceManager;
using DeviceManagementSystem.Objects.Devices;

namespace DeviceManagementSystem.Tests.ObjectsTest;

public class DeviceManagerTest
{
    private readonly string testFilePath = Path.Combine(Path.GetTempPath(), "devicesTest.txt");

    [Fact]
    public void AddDevice_ShouldAddDevice_WhenSpaceAvailable()
    {
        var manager = new DeviceManager(testFilePath);
        var device = new PersonalComputer("1", "PC-01", false);

        manager.AddDevice(device);
        Assert.Contains(device, manager.GetDevices());
    }

    [Fact]
    public void RemoveDevice_ShouldRemoveDevice_WhenDeviceExists()
    {
        var manager = new DeviceManager(testFilePath);
        var device = new PersonalComputer("1", "PC-01", false);
        manager.AddDevice(device);
        manager.RemoveDevice(device);

        Assert.DoesNotContain(device, manager.GetDevices());
    }

    [Fact]
    public void TurnOnDevice_ShouldTurnOn_WhenDeviceIsOff()
    {
        var manager = new DeviceManager(testFilePath);
        var device = new PersonalComputer("1", "PC-01", false);
        manager.AddDevice(device);
        manager.TurnOnDevice(device);

        Assert.True(device.turnOn());
    }

    [Fact]
    public void TurnOffDevice_ShouldTurnOff_WhenDeviceIsOn()
    {
        var manager = new DeviceManager(testFilePath);
        var device = new PersonalComputer("1", "PC-01", true);
        manager.AddDevice(device);
        manager.TurnOffDevice(device);

        Assert.False(device.turnOn());
    }

    [Fact]
    public void ExportDevices_ShouldCreateFile_WithDeviceData()
    {
        var manager = new DeviceManager(testFilePath);
        var device = new PersonalComputer("1", "PC-01", true);
        manager.AddDevice(device);

        string outputPath = Path.Combine(Path.GetTempPath(), "ExportTest");
        manager.ExportDevices(outputPath);

        string outputFile = Path.Combine(outputPath, "output.txt");
        Assert.True(File.Exists(outputFile));
    }

    [Fact]
    public void GetDevicesFromFile_ShouldReturnNull_WhenFileDoesNotExist()
    {
        var manager = new DeviceManager("nonExistentFile.txt");
        Assert.Null(manager.GetDevicesFromFile("nonExistentFile.txt"));
    }
}