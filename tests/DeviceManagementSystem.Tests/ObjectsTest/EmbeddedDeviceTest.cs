using DeviceManagementSystem.Objects.Devices;
using DeviceManagementSystem.Objects.Devices.DevicesExceptions;

namespace DeviceManagementSystem.Tests.ObjectsTest;

public class EmbeddedDeviceTest
{
    [Fact]
        public void Constructor_ShouldInitializeCorrectly_WithValidIp()
        {
            var device = new EmbeddedDevice("1", "TestDevice", false, "192.168.0.1", "MD Ltd. Network");

            Assert.Equal("192.168.0.1", device.Ip);
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentException_WithInvalidIp()
        {
            Assert.Throws<ArgumentException>(() => new EmbeddedDevice("1", "TestDevice", false, "999.999.999.999", "MD Ltd. Network"));
        }

        [Fact]
        public void TurnOn_ShouldReturnTrue_WhenConnectedToValidNetwork()
        {
            var device = new EmbeddedDevice("1", "TestDevice", false, "192.168.0.1", "MD Ltd. Network");

            var result = device.turnOn();

            Assert.True(result);
        }

        [Fact]
        public void TurnOn_ShouldThrowConnectionException_WhenConnectedToInvalidNetwork()
        {
            var device = new EmbeddedDevice("1", "TestDevice", false, "192.168.0.1", "Other Network");

            Assert.Throws<ConnectionException>(() => device.turnOn());
        }

        [Fact]
        public void Ip_Setter_ShouldUpdateIp_WithValidIp()
        {
            var device = new EmbeddedDevice("1", "TestDevice", false, "192.168.0.1", "MD Ltd. Network");
            device.Ip = "192.168.0.2";

            Assert.Equal("192.168.0.2", device.Ip);
        }

        [Fact]
        public void Ip_Setter_ShouldThrowArgumentException_WithInvalidIp()
        {
            var device = new EmbeddedDevice("1", "TestDevice", false, "192.168.0.1", "MD Ltd. Network");

            Assert.Throws<ArgumentException>(() => device.Ip = "999.999.999.999");
        }

        [Fact]
        public void ToString_ShouldReturnCorrectFormat()
        {
            var device = new EmbeddedDevice("1", "TestDevice", false, "192.168.0.1", "MD Ltd. Network");

            var result = device.ToString();

            Assert.Equal("ED-1,TestDevice,192.168.0.1,MD Ltd. Network", result);
        }
        
        
}