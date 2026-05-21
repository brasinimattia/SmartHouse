using BlaisePascal.SmartHouse.Domain.abstraction.Errors;
using BlaisePascal.SmartHouse.Domain.Device;
using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice;
using BlaisePascal.SmartHouse.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice
{
    public class Door:AbstractDevice, IOpenable, ILockable
    {
        public DoorStatus DoorStatus { get;private set; }
        public LockingStatus LockingStatus { get;private set; }
        public Password Password { get;private set; }
        private bool PasswordSetted => Password != null && !string.IsNullOrWhiteSpace(Password.Key);


        public Door(string name):base(name)
        {
            DoorStatus = DoorStatus.Closed;
            LockingStatus = LockingStatus.Unlocked;
            Status = DeviceStatus.On;
        }

        public Door(Guid Id, string name): base(Id, name)
        {
            DoorStatus = DoorStatus.Closed;
            LockingStatus = LockingStatus.Unlocked;
            Status = DeviceStatus.On;
        }

        public Door(string name, Guid id, DeviceStatus status, LockingStatus lockingStatus,DoorStatus doorStatus, string password, DateTime created, DateTime modified) : base(name, id, status, created, modified)
        {

            DoorStatus = doorStatus;
            LockingStatus = lockingStatus;
            Password = Password.Create(password);
        }

        public Result Open()
        {
            OnValidator();
            Result result;
            if (DoorStatus == DoorStatus.Closed && LockingStatus == LockingStatus.Unlocked)
            {
                result = Result.Success();
                DoorStatus = DoorStatus.Open;
                Touch();
            }
            else
                result = Result.Failure(DoorErrors.CannotOpenDoor);
            
            return result;

        }

        public Result Close()
        {
            OnValidator();
            Result result;
            if (!(DoorStatus == DoorStatus.Closed))
            {
                result = Result.Success();
                DoorStatus = DoorStatus.Closed;
                Touch();
            }
            else
                result = Result.Failure(DoorErrors.AlreadyClosed);
            
            return result;
        }

        public Result Lock(string key)
        {
            OnValidator();

            Result result;
            bool noPassword = !PasswordSetted;
            bool correctPassword = PasswordSetted && Password.Key == key;

            if (LockingStatus == LockingStatus.Unlocked &&
                DoorStatus == DoorStatus.Closed &&
                (noPassword || correctPassword))
            {
                result = Result.Success();
                LockingStatus = LockingStatus.Locked;
                Touch();
            }
            else
            {
                result = Result.Failure(DoorErrors.CannotLockDoor);
            }

            return result;
        }


        public Result Unlock(string key)
        {
            OnValidator();

            Result result;
            bool noPassword = !PasswordSetted;
            bool correctPassword = PasswordSetted && Password.Key == key;

            if (LockingStatus == LockingStatus.Locked &&
                (noPassword || correctPassword))
            {
                result = Result.Success();
                LockingStatus = LockingStatus.Unlocked;
                Touch();
            }
            else
            {
                result = Result.Failure(DoorErrors.CannotUnlockDoor);
            }

            return result;
        }


        public Result SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return Result.Failure(DoorErrors.CannotSetPassword);
            Password = Password.Create(password);
            Touch();
            return Result.Success();
        }


    }
}