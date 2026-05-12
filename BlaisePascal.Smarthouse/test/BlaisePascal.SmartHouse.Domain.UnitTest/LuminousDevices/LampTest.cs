using BlaisePascal.SmartHouse.Domain.abstraction;
using BlaisePascal.SmartHouse.Domain.Device;
using BlaisePascal.SmartHouse.Domain.LuminousDevices;

namespace BlaisePascal.SmartHouse.Domain.UnitTest.LuminousDevices
{
    public class LampTest
    {
        [Fact]
        public void LampConstructor_WhenTheConstructorIsCalled_IsOnIsFalseAndBrightnessIsEqualToMaxBrightness()
        {
            Lamp lamp = new Lamp("a");
            Assert.Equal(DeviceStatus.Off, lamp.Status);
            Assert.Equal(Brightness.Create(1), lamp.Brightness);

        }

        [Fact]
        public void switchIsOnOff_WhenIsOnIsFalse_ItChangesItToTrue()
        {
            Lamp lamp = new Lamp("a");
            lamp.Toggle();
            Assert.Equal(DeviceStatus.On, lamp.Status);
        }
        
        [Fact]
        public void switchIsOnOff_WhenIsOnIsTrue_ItChangesItToFalse()
        {
            Lamp lamp = new Lamp("a");
            lamp.Toggle();
            lamp.Toggle();
            Assert.Equal(DeviceStatus.Off, lamp.Status);
        }

        [Fact]

        public void increaseBrightness_WhenBrightnessIsLessThanMax_ItIncreasesByOne()
        {
            Lamp lamp = new Lamp("a");
            lamp.SwitchOn();
            lamp.DecreaseBrightness();
            lamp.IncreaseBrightness();
            Assert.Equal(Brightness.Create(2), lamp.Brightness);
        }

        [Fact]
        public void increaseBrightness_WhenBrightnessIsMax_ItRemainsMax()
        {
            Lamp lamp = new Lamp("a");
            lamp.SwitchOn();
            lamp.IncreaseBrightness();
            Assert.Equal(Brightness.Create(2), lamp.Brightness);
        }

        [Fact]
        public void decreaseBrightness_WhenBrightnessIsMoreThanMin_ItDecreasesByOne()
        {
            Lamp lamp = new Lamp("a");
            lamp.SwitchOn();
            lamp.IncreaseBrightness();
            lamp.DecreaseBrightness();
            Assert.Equal(Brightness.Create(1), lamp.Brightness);
        }
        
        [Fact]
        public void decreaseBrightness_WhenBrightnessIsMin_ItRemainsMin()
        {
            Lamp lamp = new Lamp("a");
            lamp.SwitchOn();
            for (int i = 0; i < 11; i++)
            {
                lamp.DecreaseBrightness();
            }
            Assert.Equal(Brightness.Create(1), lamp.Brightness);
        }

        [Fact]
        public void ChangeBrightness_WhenTheParameterIsGreaterThan0AndLowerThan11_BrightnessChangesToParameter()
        {
            Lamp lamp = new Lamp("a");
            lamp.SwitchOn();
            lamp.ChangeBrightness(7);
            Assert.Equal(Brightness.Create(7), lamp.Brightness);
        }



    }
}