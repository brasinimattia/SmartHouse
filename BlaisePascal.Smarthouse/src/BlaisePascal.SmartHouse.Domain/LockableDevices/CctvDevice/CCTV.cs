using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.Device;
using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice;

namespace BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice
{
    public class CCTV : AbstractDevice, ICCTV, ILockable
    {
        //Properties
        public CCTVMode Mode { get; private set; }
        public bool isRecording { get; private set; }
        public LockingStatus LockingStatus { get; private set; }
        public Password Password { get; private set; }
        private bool PasswordSetted => !string.IsNullOrWhiteSpace(Password.Key);

        //Constructor
        public CCTV(string name) : base(name)
        {
            Mode = CCTVMode.NoMode;
            isRecording = false;
        }
        public CCTV(string name, Guid id) : base(id, name)
        {
            Mode = CCTVMode.NoMode;
            isRecording = false;
        }
        public CCTV(string name, Guid id, DeviceStatus status, CCTVMode mode, bool recording, LockingStatus lockingStatus, string password, DateTime created, DateTime modified): base(name, id, status, created, modified)
        {
            Mode = mode;
            isRecording = recording;
            LockingStatus = lockingStatus;
            Password = Password.Create(password);
        }

        //Methods
        public override void SwitchOn()
        {
            if (Status == DeviceStatus.On)
                throw new Exception("The CCTV is already on");

            Status = DeviceStatus.On;
            Mode = CCTVMode.Normal;
            LastModifiedAtUtc = DateTime.Now;
        }

        public override void SwitchOff()
        {
            if (Status == DeviceStatus.Off)
                throw new Exception("The CCTV is already off");

            Status = DeviceStatus.Off;
            Mode = CCTVMode.NoMode;
            LastModifiedAtUtc = DateTime.Now;
        }

        public void SetNormalMode()
        {
            OnValidator();
            if (Mode == CCTVMode.Normal)
                throw new Exception("The mode is already normal");
            Mode = CCTVMode.Normal;
            LastModifiedAtUtc = DateTime.Now;
        }

        public void SetNightMode()
        {
            OnValidator();
            if (Mode == CCTVMode.Night)
                throw new Exception("The mode is already night");
            Mode = CCTVMode.Night;
            LastModifiedAtUtc = DateTime.Now;
        }

        public void SetMode(CCTVMode mode)
        {
            OnValidator();
            if (Mode == mode)
                throw new ArgumentException($"The mode is already {mode}");
            Mode = mode;
            LastModifiedAtUtc = DateTime.Now;
        }

        public void StartRecording()
        {
            OnValidator();
            if (isRecording)
                throw new Exception("The cctv is already recording");
            isRecording = true;
            LastModifiedAtUtc = DateTime.Now;
        }

        public void StopRecording()
        {
            OnValidator();
            if (!isRecording)
                throw new Exception("The cctv is not recording");
            isRecording = false;
            LastModifiedAtUtc = DateTime.Now;
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty or whitespace");
            Password = Password.Create(password);
            LastModifiedAtUtc = DateTime.Now;
        }

        public void Unlock(string key)
        {
            OnValidator();

            bool noPassword = !PasswordSetted;
            bool correctPassword = PasswordSetted && Password.Key == key;

            if (LockingStatus == LockingStatus.Locked &&
                (noPassword || correctPassword))
            {
                LockingStatus = LockingStatus.Unlocked;
            }
            else
            {
                throw new Exception("cannot unlock the CCTV");
            }

            LastModifiedAtUtc = DateTime.Now;
        }


        public void Lock(string key)
        {
            OnValidator();

            bool noPassword = !PasswordSetted;
            bool correctPassword = PasswordSetted && Password.Key == key;

            if (LockingStatus == LockingStatus.Unlocked &&
                (noPassword || correctPassword))
            {
                LockingStatus = LockingStatus.Locked;
            }
            else
            {
                throw new Exception("cannot lock the CCTV");
            }

            LastModifiedAtUtc = DateTime.Now;
        }

    }
}
