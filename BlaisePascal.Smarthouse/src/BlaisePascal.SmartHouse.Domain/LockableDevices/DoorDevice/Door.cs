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
            }
            else
                result = Result.Failure(DoorErrors.CannotOpenDoor);
            Touch();
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
            }
            else
                result = Result.Failure(DoorErrors.AlreadyClosed);
            Touch();
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
            }
            else
            {
                result = Result.Failure(DoorErrors.CannotLockDoor);
            }

            Touch();
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
            }
            else
            {
                result = Result.Failure(DoorErrors.CannotUnlockDoor);
            }

            Touch();
            return result;
        }


        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty or whitespace");
            Password = Password.Create(password);
            LastModifiedAtUtc = DateTime.Now;
        }


    }
}