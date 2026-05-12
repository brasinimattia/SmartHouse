using BlaisePascal.SmartHouse.Domain.abstraction;
using BlaisePascal.SmartHouse.Domain.Device;
using BlaisePascal.SmartHouse.Domain.LuminousDevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.UnitTest.LuminousDevices
{
    public class EcoLampTest
    {
        [Fact]
        public void constructor_WhenLampIsCreated_IsOnIsFalseAndBrightnessIsHisMaxValue()
        {
            EcoLamp lamp = new EcoLamp("a");
            Assert.Equal(DeviceStatus.Off, lamp.Status);
            Assert.Equal(Brightness.Create(1), lamp.Brightness);
        }

        [Fact]
        public void switchOnOff_WhenIsOnIsFalse_ItChangesItToTrue()
        {
            EcoLamp lamp = new EcoLamp("a");
            lamp.Toggle();
            Assert.Equal(DeviceStatus.On, lamp.Status);
        }

        [Fact]
        public void switchOnOff_WhenIsOnIsTrue_ItChangesItToFalse()
        {
            EcoLamp lamp = new EcoLamp("a");
            lamp.Toggle();
            lamp.Toggle();
            Assert.Equal(DeviceStatus.Off, lamp.Status);
        }

        [Fact]
        public void increaseBrightness_WhenBrightnessIsMax_ItDoesNotIncrease()
        {
            EcoLamp lamp = new EcoLamp("a");
            lamp.SwitchOn();
            lamp.IncreaseBrightness();
            Assert.Equal(Brightness.Create(2), lamp.Brightness);
        }

        [Fact]
        public void increaseBrightness_WhenBrightnessIsLessThanMax_ItIncreasesByOne()
        {
            EcoLamp lamp = new EcoLamp("a");
            lamp.SwitchOn();
            lamp.DecreaseBrightness();
            lamp.IncreaseBrightness();
            Assert.Equal(Brightness.Create(2), lamp.Brightness);
        }

        [Fact]
        public void decreaseBrightness_WhenBrightnessIsMoreThanMinBrightness_ItDecreasesByOne()
        {
            EcoLamp lamp = new EcoLamp("a");
            lamp.SwitchOn();
            lamp.IncreaseBrightness();
            lamp.DecreaseBrightness();
            Assert.Equal(Brightness.Create(1), lamp.Brightness);
        }

        [Fact]
        public void decreaseBrightness_WhenBrightnessIsMin_ItDoesNotDecrease()
        {
            EcoLamp lamp = new EcoLamp("a");
            lamp.SwitchOn();
            for (int i = 0; i < 5; i++)
            {
                lamp.DecreaseBrightness();
            }

            Assert.Equal(Brightness.Create(1), lamp.Brightness);
        }


        [Fact]
        public void changeBrightness_WhenNewBrightnessIsInsideTheRange_AssignBightnessCorrectly()
        {
            EcoLamp lamp = new EcoLamp("a");
            lamp.SwitchOn();
            lamp.ChangeBrightness(3);
            Assert.Equal(Brightness.Create(3), lamp.Brightness);
        }

        [Fact]
        public void checkAutoOff_WhenAutoOffTimeIsNotReached_LampRemainsOn()
        {
            EcoLamp lamp = new EcoLamp("a");
            lamp.SwitchOn();
            lamp.CheckAutoOff();
            Assert.Equal(DeviceStatus.On, lamp.Status);
        }

        [Fact]
        public void changeBrightness_IncreasesAutoOffTime()
        {
            EcoLamp lamp = new EcoLamp("a");
            lamp.SwitchOn();
            lamp.ChangeBrightness(2);
            lamp.CheckAutoOff();
            Assert.Equal(DeviceStatus.On, lamp.Status);
        }
        [Fact]
        public void increaseBrightness_IncreasesAutoOffTime()
        {
            EcoLamp lamp = new EcoLamp("a");
            lamp.SwitchOn();
            lamp.IncreaseBrightness();
            lamp.CheckAutoOff();
            Assert.Equal(DeviceStatus.On, lamp.Status);
        }
        [Fact]
        public void decreaseBrightness_IncreasesAutoOffTime()
        {
            EcoLamp lamp = new EcoLamp("a");
            lamp.SwitchOn();
            lamp.DecreaseBrightness();
            lamp.CheckAutoOff();
            Assert.Equal(DeviceStatus.On, lamp.Status);
        }
        
    }
}
