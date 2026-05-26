using BlaisePascal.SmartHouse.Domain.abstraction;
using BlaisePascal.SmartHouse.Domain.abstraction.Errors;
using BlaisePascal.SmartHouse.Domain.abstraction.Events;
using BlaisePascal.SmartHouse.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlaisePascal.SmartHouse.Domain.Device
{
    public abstract class AbstractDevice :Entity, ISwitchable
    {
        public Name Name { get; protected set; }
        public DeviceStatus Status { get; protected set; }
        public DateTime CreatedAtUtc { get; protected set; }
        public DateTime LastModifiedAtUtc { get; protected set; }

        protected AbstractDevice(string name)
        {
            Id = Guid.NewGuid();
            Name = Name.Create(name);
            Status = DeviceStatus.Off;
            CreatedAtUtc = DateTime.Now;
            Touch();
        }
        public AbstractDevice(Guid guid, string name)
        {
            CreatedAtUtc = DateTime.Now;
            Touch();
            Status = DeviceStatus.Off;
            Id = guid;
            Name = Name.Create(name);
        }
        public AbstractDevice() { }

        public AbstractDevice(string name, Guid id, DeviceStatus status, DateTime created, DateTime modified)
        {
            Name = Name.Create(name);
            Id = id;
            Status = status;
            CreatedAtUtc = created;
            LastModifiedAtUtc = modified;
        }

        public virtual Result SwitchOn()
        {
            if (Status == DeviceStatus.On)
                return Result.Failure(LampErrors.AlreadyOn);

            Status = DeviceStatus.On;

            Raise(new DeviceSwitchedOnEvent(Id));
            Touch();

            return Result.Success();
        }

        //Methods
        public void OnValidator()
        {
            if (Status == DeviceStatus.Off)
                throw new Exception("The device is off");
            
        }
        public virtual Result Toggle()
        {
            if (Status == DeviceStatus.On)
                SwitchOff();
            else
                SwitchOn();
            return Result.Success();
            Touch();
        }

        public virtual Result SwitchOff()
        {
            if (Status == DeviceStatus.On)
                return Result.Failure(LampErrors.AlreadyOff);

            Status = DeviceStatus.On;

            Raise(new DeviceSwitchedOnEvent(Id));
            Touch();

            return Result.Success();
        }

        public virtual Result SetNewName(string newName)
        {
             if (newName == Name.String)
            {
                throw new Exception("name cannot be the same as the old one");
            }
            Name = Name.Create(newName);
            LastModifiedAtUtc = DateTime.Now;

            return Result.Success();
        }

        protected void Touch() 
        {
            LastModifiedAtUtc = DateTime.UtcNow;
        }

    }
}