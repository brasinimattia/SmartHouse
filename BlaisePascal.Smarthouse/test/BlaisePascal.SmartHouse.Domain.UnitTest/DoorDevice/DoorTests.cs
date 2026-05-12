using BlaisePascal.SmartHouse.Domain.Device;
using BlaisePascal.SmartHouse.Domain.LockableDevices;
using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.UnitTest.DoorDevice
{
    public class DoorTests
    {
        
        [Fact]
        public void Constructor_WhenDoorIsCreated_StatusIsClosedLockingStatusIsUnlockedAndFunctioningStatusIsOn()
        {
            Door door = new Door("Front Door");
            Assert.Equal(DoorStatus.Closed, door.DoorStatus);
            Assert.Equal(LockingStatus.Unlocked, door.LockingStatus);
            Assert.Equal(DeviceStatus.On, door.Status);
        }

        [Fact]
        public void OpenTheDoor_WhenDoorIsClosed_ItChangesStatusToOpen()
        {
            Door door = new Door("Front Door");
            door.Open();
            Assert.Equal(DoorStatus.Open, door.DoorStatus);
        }

        [Fact]
        public void CloseTheDoor_WhenDoorIsOpen_ItChangesStatusToClosed()
        {
            Door door = new Door("Front Door");
            door.Open();
            door.Close();
            Assert.Equal(DoorStatus.Closed, door.DoorStatus);
        }

        [Fact]
        public void LockTheDoor_WhenDoorIsClosed_ItChangesLockingStatusToLocked()
        {
            Door door = new Door("Front Door");
            door.Lock("password1");
            Assert.Equal(LockingStatus.Locked, door.LockingStatus);
        }

        [Fact]
        public void UnlockTheDoor_WhenDoorIsLocked_ItChangesLockingStatusToUnlocked()
        {
            Door door = new Door("Front Door");
            door.Lock(null);
            door.Unlock(null);
            Assert.Equal(LockingStatus.Unlocked, door.LockingStatus);
        }

        [Fact]
        public void OpenTheDoor_WhenDoorIsLocked_ItThrowsException()
        {
            Door door = new Door("Front Door");
            door.Lock(null);
            Assert.Throws<Exception>(() => door.Open());
        }

        [Fact]
        public void LockTheDoor_WhenDoorIsOpen_ItThrowsException()
        {
            Door door = new Door("Front Door");
            door.Open();
            Assert.Throws<Exception>(() => door.Lock(null));
        }

        [Fact]
        public void UnlockTheDoor_WhenDoorIsUnlocked_ItThrowsException()
        {
            Door door = new Door("Front Door");
            Assert.Throws<Exception>(() => door.Unlock(null));
        }

        [Fact]
        public void OpenTheDoor_WhenDoorIsAlreadyOpen_ItThrowsException()
        {
            Door door = new Door("Front Door");
            door.Open();
            Assert.Throws<Exception>(() => door.Open());
        }

        [Fact]
        public void CloseTheDoor_WhenDoorIsAlreadyClosed_StatusDoesNotChange()
        {
            Door door = new Door("Front Door");
            Assert.Equal(DoorStatus.Closed, door.DoorStatus);
        }

        [Fact]
        public void LockTheDoor_WhenDoorIsAlreadyLocked_ItThrowsException()
        {
            Door door = new Door("Front Door");
            door.Lock(null);
            Assert.Throws<Exception>(() => door.Lock(null));
        }

        [Fact]
        public void TurnOn_WhenStatusIsOff_ItChangesToOn()
        {
            Door door = new Door("Front Door");
            door.SwitchOff();
            door.SwitchOn();
            Assert.Equal(DeviceStatus.On, door.Status);
        }

        [Fact]
        public void TurnOff_WhenStatusIsOn_ItChangesToOff()
        {
            Door door = new Door("Front Door");
            door.SwitchOff();
            Assert.Equal(DeviceStatus.Off, door.Status);
        }



        [Fact]
        public void SetNewName_WhenCalled_NameIsUpdated()
        {
            Door door = new Door("Front Door");
            door.SetNewName("Back Door");
            Assert.Equal("Back Door", door.Name.String);
        }

        [Fact]
        public void SetNewName_WhenNewNameIsEmpty_ThrowsException()
        {
            Door door = new Door("Front Door");
            Assert.Throws<ArgumentException>(() => door.SetNewName(""));
        }

        [Fact]
        public void SetNewName_WhenNewNameIsNull_ThrowsException()
        {
            Door door = new Door("Front Door");
            Assert.Throws<ArgumentException>(() => door.SetNewName(null));
        }

        [Fact]
        public void SetNewName_WhenNewNameIsTheSameAsCurrent_NameDoesNotChange()
        {
            Door door = new Door("Front Door");
            Assert.Throws<Exception>(() => door.SetNewName("Front Door"));
        }

    }
}
