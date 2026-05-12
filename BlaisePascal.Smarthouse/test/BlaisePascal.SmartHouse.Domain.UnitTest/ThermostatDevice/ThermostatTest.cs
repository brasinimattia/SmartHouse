using BlaisePascal.SmartHouse.Domain.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.TemperatureDevices.ThermostatDevice;


namespace BlaisePascal.SmartHouse.Domain.UnitTest.ThermostatDevice
{
    public class ThermostatTest
    {
        //TODO debug 3 tests
        [Fact]
        public void SetTemperatureToReach_WhenTemperatureIsWithinRange_ShouldSetTemperature()
        {
            // Arrange
            Thermostat thermostat = new Thermostat("Living Room Thermostat");
            thermostat.SwitchOn();
            int validTemperature = 25;
            // Act
            thermostat.SetTemperatureToReach(validTemperature);
            // Assert
            Assert.Equal(validTemperature, thermostat.Temperature.Value);
        }
        [Fact]
        public void SetTemperatureToReach_WhenTemperatureIsOutOfRange_SetToMax()
        {
            // Arrange
            Thermostat thermostat = new Thermostat("Living Room Thermostat");
            thermostat.SwitchOn();
            int invalidTemperature = 50; // Assuming max temperature is 40
            thermostat.SetTemperatureToReach(invalidTemperature);
            // Act & Assert
            Assert.Equal(thermostat.Temperature.Max, thermostat.Temperature.Value);
        }
        [Fact]
        public void IncreaseTemperatureToReach_WhenCalled_ShouldIncreaseTemperatureByStep()
        {
            // Arrange
            Thermostat thermostat = new Thermostat("Living Room Thermostat");
            thermostat.SwitchOn();
            double initialTemperature = thermostat.Temperature.Value ;
            // Act
            thermostat.IncreaseTemperatureToReach();
            // Assert
            Assert.Equal(initialTemperature + thermostat.TemperatureStep, thermostat.Temperature.Value);
        }
        [Fact]
        public void IncreaseTemperatureToReach_WhenAtMaxTemperature_ShouldNotExceedMax()
        {
            // Arrange
            Thermostat thermostat = new Thermostat("Living Room Thermostat");
            thermostat.SwitchOn();
            thermostat.SetTemperatureToReach(thermostat.Temperature.Max);
            // Act
            thermostat.IncreaseTemperatureToReach();
            // Assert
            Assert.Equal(thermostat.Temperature.Max, thermostat.Temperature.Value);
        }
        [Fact]
        public void DecreaseTemperatureToReach_WhenAtMinTemperature_ShouldNotGoBelowMin()
        {
            // Arrange
            Thermostat thermostat = new Thermostat("Living Room Thermostat");
            thermostat.SwitchOn();
            thermostat.SetTemperatureToReach(thermostat.Temperature.Min);
            // Act
            thermostat.DecreaseTemperatureToReach();
            // Assert
            Assert.Equal(thermostat.Temperature.Min, thermostat.Temperature.Value);
        }
        [Fact]
        public void SetTemperatureToReach_WhenDeviceIsOff_ShouldThrowException()
        {
            // Arrange
            Thermostat thermostat = new Thermostat("Living Room Thermostat");
            int validTemperature = 25;
            // Act & Assert
            Assert.Throws<Exception>(() => thermostat.SetTemperatureToReach(validTemperature));
        }
        [Fact]
        public void IncreaseTemperatureToReach_WhenDeviceIsOff_ShouldThrowException()
        {
            // Arrange
            Thermostat thermostat = new Thermostat("Living Room Thermostat");
            // Act & Assert
            Assert.Throws<Exception>(() => thermostat.IncreaseTemperatureToReach());
        }
        [Fact]
        public void DecreaseTemperatureToReach_WhenDeviceIsOff_ShouldThrowException()
        {
            // Arrange
            Thermostat thermostat = new Thermostat("Living Room Thermostat");
            // Act & Assert
            Assert.Throws<Exception>(() => thermostat.DecreaseTemperatureToReach());
        }
        [Fact]
        public void SetFahrenheitMode_WhenAlreadyInFahrenheit_ShouldThrowException()
        {
            // Arrange
            Thermostat thermostat = new Thermostat("Living Room Thermostat");
            thermostat.SwitchOn();
            thermostat.SetFahrenheitMode();
            // Act & Assert
            Assert.Throws<Exception>(() => thermostat.SetFahrenheitMode());
        }
        [Fact]
        public void SetCelsiusMode_WhenAlreadyInCelsius_ShouldThrowException()
        {
            // Arrange
            Thermostat thermostat = new Thermostat("Living Room Thermostat");
            thermostat.SwitchOn();
            // Act & Assert
            Assert.Throws<Exception>(() => thermostat.SetCelsiusMode());
        }
        [Fact]
        public void SetFahrenheitMode_WhenCalled_ShouldConvertTemperaturesToFahrenheit()
        {
            // Arrange
            Thermostat thermostat = new Thermostat("Living Room Thermostat");
            thermostat.SwitchOn();
            double initialTemperature = thermostat.Temperature.Value;
            // Act
            thermostat.SetFahrenheitMode();
            // Assert
            double expectedTemperature = (initialTemperature -32 ) *5/9;
            Assert.Equal(expectedTemperature, thermostat.Temperature.Value);
        }
        [Fact]
        public void SetCelsiusMode_WhenCalled_ShouldConvertTemperaturesToCelsius()
        {
            // Arrange
            Thermostat thermostat = new Thermostat("Living Room Thermostat");
            thermostat.SwitchOn();
            thermostat.SetFahrenheitMode();
            // Act
            double expectedTemperature = (thermostat.Temperature.Value * 9 / 5) + 32;
            thermostat.SetCelsiusMode();
            // Assert
            Assert.Equal(expectedTemperature, thermostat.Temperature.Value);
        }
        [Fact]
        public void SetCelsiusMode_AfterSettingFahrenheit_ShouldRestoreOriginalCelsiusTemperature()
        {
            // Arrange
            Thermostat thermostat = new Thermostat("Living Room Thermostat");
            thermostat.SwitchOn();
            double originalCelsiusTemperature = thermostat.Temperature.Value;
            // Act
            thermostat.SetFahrenheitMode();
            thermostat.SetCelsiusMode();
            // Assert
            Assert.Equal(originalCelsiusTemperature, thermostat.Temperature.Value);
        }
        [Fact]
        public void SetFahrenheitMode_WhenDeviceIsOff_ShouldThrowException()
        {
            // Arrange
            Thermostat thermostat = new Thermostat("Living Room Thermostat");
            // Act & Assert
            Assert.Throws<Exception>(() => thermostat.SetFahrenheitMode());
        }
        [Fact]
        public void SetCelsiusMode_WhenDeviceIsOff_ShouldThrowException()
        {
            // Arrange
            Thermostat thermostat = new Thermostat("Living Room Thermostat");
            // Act & Assert
            Assert.Throws<Exception>(() => thermostat.SetCelsiusMode());
        }


    }
}
