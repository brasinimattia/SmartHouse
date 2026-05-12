using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.Device;

namespace BlaisePascal.SmartHouse.Domain.LuminousDevices
{
    public class TwoLampDevice
    {
        //Properties
        public AbstractLamp Lamp1 { get; private set; }
        public AbstractLamp Lamp2 { get; private set; }

        //Constructor
        public TwoLampDevice(AbstractLamp lamp1, AbstractLamp lamp2)
        {
            Lamp1 = lamp1;
            Lamp2 = lamp2;
        }


        //Methods
        public void ToggleOneLamp(Guid Id)
        {
            if(Lamp1.Id == Id)
            {
                Lamp1.Toggle();
            }
            else if(Lamp2.Id == Id)
            {
                Lamp2.Toggle();
            }
        }

        public void ToggleOneLamp(string name)
        {
            if (Lamp1.Name.String == name)
            {
                Lamp1.Toggle();
            }
            else if (Lamp2.Name.String == name)
            {
                Lamp2.Toggle();
            }
        }

        public void TurnBothOn()
        {
            if (Lamp1.Status == DeviceStatus.Off)
                Lamp1.Toggle();
            if (Lamp2.Status == DeviceStatus.Off)
                Lamp2.Toggle();
        }

        public void TurnBothOff()
        {
            if (Lamp1.Status == DeviceStatus.On)
                Lamp1.Toggle();
            if (Lamp2.Status == DeviceStatus.On)
                Lamp2.Toggle();
        }

        public void IncreaseBothBrightness()
        {
            Lamp1.IncreaseBrightness();
            Lamp2.IncreaseBrightness();
        }

        public void DecreaseBothBrightness()
        {
            Lamp1.DecreaseBrightness();
            Lamp2.DecreaseBrightness();
        }

        public void IncreaseOneBrightness(Guid Id)
        {
            if (Lamp1.Id == Id)
            {
                Lamp1.IncreaseBrightness();
            }
            else if (Lamp2.Id == Id)
            {
                Lamp2.IncreaseBrightness();
            }
        }

        public void IncreaseOneBrightness(string name)
        {
            if (Lamp1.Name.String == name)
            {
                Lamp1.IncreaseBrightness();
            }
            else if (Lamp2.Name.String == name)
            {
                Lamp2.IncreaseBrightness();
            }
        }

        public void DecreaseOneBrightness(Guid Id)
        {
            if (Lamp1.Id == Id)
            {
                Lamp1.DecreaseBrightness();
            }
            else if (Lamp2.Id == Id)
            {
                Lamp2.DecreaseBrightness();
            }
        }

        public void DecreaseOneBrightness(string name)
        {
            if (Lamp1.Name.String == name)
            {
                Lamp1.IncreaseBrightness();
            }
            else if (Lamp2.Name.String == name)
            {
                Lamp2.IncreaseBrightness();
            }
        }

        public void ChangeOneBrightness(Guid Id, int newBrightness)
        {
            if (Lamp1.Id == Id)
                Lamp1.ChangeBrightness(newBrightness);
            else if (Lamp2.Id == Id)
                Lamp2.ChangeBrightness(newBrightness);
        }

        public void ChangeOneBrightness(string name, int newBrightness)
        {
            if (Lamp1.Name.String == name)
                Lamp1.ChangeBrightness(newBrightness);
            else if (Lamp2.Name.String == name)
                Lamp2.ChangeBrightness(newBrightness);
        }
    }
}
