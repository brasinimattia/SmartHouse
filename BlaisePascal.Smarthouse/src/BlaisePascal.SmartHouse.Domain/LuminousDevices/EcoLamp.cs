using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.Device;
using BlaisePascal.SmartHouse.SharedKernel;

namespace BlaisePascal.SmartHouse.Domain.LuminousDevices
{
    public class EcoLamp: AbstractLamp
    {
        //Const
        private const int DefaultAutoOffMinutes = 10;
        private const int MinAutoOffMinutes = 1;

        //Properties
        private DateTime? autoOffAtUtc;

        //Constructor
        public EcoLamp(string name ): base( name ) { }
       
        public EcoLamp(Guid newID, string name): base( newID, name ) { }

        public override Result SwitchOn()
        {
            var result = base.SwitchOn();
            if (result.IsSuccess)
            {
                autoOffAtUtc = DateTime.Now.AddMinutes(DefaultAutoOffMinutes);
            }
            return result;
        }
        
        public override Result SwitchOff()
        {
            var result = base.SwitchOff();
            if (result.IsSuccess)
            {
                autoOffAtUtc = DateTime.Now.AddMinutes(DefaultAutoOffMinutes);
            }
            return result;
        }
        public override Result ChangeBrightness(int value)
        {
            var result = base.ChangeBrightness(value);
            if (result.IsSuccess)
            {
                ResetAutoOffIfNeeded();
            }
            return result;
        }

        public override Result DecreaseBrightness()
        {
            var result = base.DecreaseBrightness();
            if (result.IsSuccess)
            {
                ResetAutoOffIfNeeded();
            }
            return result;
        }

        public override Result IncreaseBrightness()
        {
            var result = base.IncreaseBrightness();
            if (result.IsSuccess)
            {
                ResetAutoOffIfNeeded();
            }
            return result;
        }

        public void CheckAutoOff()
        {
            if (Status == DeviceStatus.On &&
                autoOffAtUtc.HasValue &&
                DateTime.Now >= autoOffAtUtc.Value)
            {
                SwitchOff();
            }
        }

        private void ResetAutoOffIfNeeded()
        {
            if (autoOffAtUtc.HasValue)
                autoOffAtUtc = DateTime.Now.AddMinutes(DefaultAutoOffMinutes);
        }
    }
}
