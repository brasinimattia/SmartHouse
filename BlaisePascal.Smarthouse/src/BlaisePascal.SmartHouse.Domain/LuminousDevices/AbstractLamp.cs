using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.abstraction;
using BlaisePascal.SmartHouse.Domain.abstraction.Errors;
using BlaisePascal.SmartHouse.Domain.abstraction.Events;
using BlaisePascal.SmartHouse.Domain.Device;
using BlaisePascal.SmartHouse.SharedKernel;

namespace BlaisePascal.SmartHouse.Domain.LuminousDevices
{
    public abstract class AbstractLamp: AbstractDevice, ILuminous
    {
        //Properties
        public Brightness Brightness { get; protected set; }
       
        //Constructor
        protected AbstractLamp() { }
        protected AbstractLamp(string name):base(name)
        {
            Brightness = Brightness.Create(Brightness.Min);
        }
        protected AbstractLamp(Guid Id, string name): base(Id, name)
        {
            Brightness = Brightness.Create(Brightness.Min);
        }
        protected AbstractLamp(string name, Guid id, DeviceStatus status, DateTime created, DateTime modified, int brightness): base(name, id, status, created, modified)
        {
            Brightness = Brightness.Create(brightness);
        }

        //Methods

        public override Result SwitchOn()
        {
            var result = base.SwitchOn();
            if (result.IsFailure)
                return result;

           /*var brightnessResult = Brightness.Create(Brightness.Min);
            if (brightnessResult.IsFailure)
                return Result.Failure(brightnessResult.Error);*/
            Raise(new DeviceSwitchedOnEvent(Id));
            Touch();

            return Result.Success();
        }

        public override Result SwitchOff()
        {
            var result = base.SwitchOff();
            if (result.IsFailure)
                return result;
            Raise(new DeviceSwitchedOffEvent(Id));
            Touch();

            return Result.Success();
        }

        public virtual Result IncreaseBrightness()
        {
            if (Status == DeviceStatus.Off)
                Result.Failure(LampErrors.IsOff);
            int newValue = Brightness.Value + 1;
            Brightness = Brightness.Create(newValue);
            Touch();

            return Result.Success();
        }

        public  virtual Result DecreaseBrightness()
        {
            if (Status == DeviceStatus.Off)
                Result.Failure(LampErrors.IsOff);
            int newValue = Brightness.Value - 1;
            Brightness = Brightness.Create(newValue);
            Touch();

            return Result.Success();
        }

        public virtual Result ChangeBrightness(int brightness)
        {
            if (Status == DeviceStatus.Off)
                Result.Failure(LampErrors.IsOff);
            Brightness = Brightness.Create(brightness);
            Touch();

            return Result.Success();
        }
    }
}
