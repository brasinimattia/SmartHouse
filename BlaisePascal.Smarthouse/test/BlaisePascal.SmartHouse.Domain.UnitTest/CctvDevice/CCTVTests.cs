using BlaisePascal.SmartHouse.Domain.Device;
using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.UnitTest.CctvDevice
{
    public class CCTVTests
    {
        [Fact]
        public void Constructor_AfterCreating_TheCctvIsOff()
        {
            //Arrange
            CCTV cctv = new CCTV("a");
            //Act & Assert
            Assert.Equal(DeviceStatus.Off, cctv.Status);
            Assert.Equal(CCTVMode.NoMode, cctv.Mode);
            Assert.False(cctv.isRecording);
        }

        [Fact]
        public void TurnOn_WhenCctvIsOff_TurnItOn()
        {
            //Arrange
            CCTV cctv = new CCTV("a");
            //Act
            cctv.SwitchOn();
            //Assert
            Assert.Equal(DeviceStatus.On, cctv.Status);
        }

        [Fact]
        public void TurnOn_WhenCctvIsOn_ThrowError()
        {
            //Arrange
            CCTV cctv = new CCTV("a");
            //Act
            cctv.SwitchOn();
            //Assert
            Assert.Throws<Exception>(() => cctv.SwitchOn());
        }


        [Fact]
        public void TurnOff_WhenCctvIsOn_TurnItOn()
        {
            //Arrange
            CCTV cctv = new CCTV("a");
            //Act
            cctv.SwitchOn();
            cctv.SwitchOff();
            //Assert
            Assert.Equal(DeviceStatus.Off, cctv.Status);
        }

        [Fact]
        public void TurnOff_WhenCctvIsOff_ThrowError()
        {
            //Arrange
            CCTV cctv = new CCTV("a");
            //Act & Assert
            Assert.Throws<Exception>(() => cctv.SwitchOff());
        }

        [Fact]
        public void SetNormalMode_WhenCctvIsOff_ThrowError()
        {
            //Arrange
            CCTV cctv = new CCTV("a");
            //Act & Assert
            Assert.Throws<Exception>(() => cctv.SetNormalMode());
        }

        [Fact]
        public void SetNormalMode_WhenModeIsAlreadyNormal_ThrowError()
        {
            //Arrange
            CCTV cctv = new CCTV("a");
            //Act
            cctv.SwitchOn();
            //Assert
            Assert.Throws<Exception>(() => cctv.SetNormalMode());
        }

        [Fact]
        public void SetNormalMode_WhenModeNotNormal_SetItToNormal()
        {
            //Arrange
            CCTV cctv = new CCTV("a");
            //Act
            cctv.SwitchOn();
            cctv.SetNightMode();
            cctv.SetNormalMode();
            //Assert
            Assert.Equal(CCTVMode.Normal, cctv.Mode);
        }

        [Fact]
        public void SetNightMode_WhenCctvIsOff_ThrowError()
        {
            //Arrange
            CCTV cctv = new CCTV("a");
            //Act & Assert
            Assert.Throws<Exception>(() => cctv.SetNightMode());
        }

        [Fact]
        public void SetNightMode_WhenModeIsAlreadyNight_ThrowError()
        {
            //Arrange
            CCTV cctv = new CCTV("a");
            //Act
            cctv.SwitchOn();
            cctv.SetNightMode();
            //Assert
            Assert.Throws<Exception>(() => cctv.SetNightMode());
        }

        [Fact]
        public void SetNightMode_WhenModeNotNight_SetItToNight()
        {
            //Arrange
            CCTV cctv = new CCTV("a");
            //Act
            cctv.SwitchOn();
            cctv.SetNightMode();
            //Assert
            Assert.Equal(CCTVMode.Night, cctv.Mode);
        }

        [Fact]
        public void SetMode_WhenCctvIsOff_ThrowError()
        {
            //Arrange
            CCTV cctv = new CCTV("a");
            //Act & Assert
            Assert.Throws<Exception>(() => cctv.SetMode(CCTVMode.Normal));
        }

        [Fact]
        public void SetMode_WhenCctvIsOnTheModeChoosed_ThrowError()
        {
            //Arrange
            CCTV cctv = new CCTV("a");
            //Act
            cctv.SwitchOn();
            //Assert
            Assert.Throws<ArgumentException>(() => cctv.SetMode(CCTVMode.Normal));
        }

        [Fact]
        public void SetMode_WhenCctvIsNotOnTheModeChoosed_ChangeMode()
        {
            //Arrange
            CCTV cctv = new CCTV("a");
            //Act
            cctv.SwitchOn();
            cctv.SetMode(CCTVMode.Night);
            //Assert
            Assert.Equal(CCTVMode.Night, cctv.Mode);
        }

        [Fact]
        public void StartRecording_WhenCctvIsOff_ThrowError()
        {
            //Arrange
            CCTV cctv = new CCTV("a");
            //Act & Assert
            Assert.Throws<Exception>(() => cctv.StartRecording());
        }

        [Fact]
        public void StartRecording_WhenCctvIsAlreadyRecording_ThrowError()
        {
            //Arrange
            CCTV cctv = new CCTV("a");
            //Act
            cctv.SwitchOn();
            cctv.StartRecording();
            //Assert
            Assert.Throws<Exception>(() => cctv.StartRecording());
        }

        [Fact]
        public void StartRecording_WhenCctvIsNotRecording_StartTheRecording()
        {
            //Arrange
            CCTV cctv = new CCTV("a");
            //Act
            cctv.SwitchOn();
            cctv.StartRecording();
            //Assert
            Assert.True(cctv.isRecording);
        }

        [Fact]
        public void StopRecording_WhenCctvIsOff_ThrowError()
        {
            //Arrange
            CCTV cctv = new CCTV("a");
            //Act & Assert
            Assert.Throws<Exception>(() => cctv.StopRecording());
        }

        [Fact]
        public void StopRecording_WhenCctvIsNotRecording_ThrowError()
        {
            //Arrange
            CCTV cctv = new CCTV("a");
            //Act
            cctv.SwitchOn();
            //Assert
            Assert.Throws<Exception>(() => cctv.StopRecording());
        }

        [Fact]
        public void StopRecording_WhenCctvIsRecording_StopTheRecording()
        {
            //Arrange
            CCTV cctv = new CCTV("a");
            //Act
            cctv.SwitchOn();
            cctv.StartRecording();
            cctv.StopRecording();
            //Assert
            Assert.False(cctv.isRecording);
        }
    }
}
