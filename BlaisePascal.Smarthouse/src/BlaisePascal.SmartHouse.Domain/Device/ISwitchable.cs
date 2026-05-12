using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Device
{
    public interface ISwitchable
    {
        void TurnOn();
        void SwitchOff();
        void Toggle();
        void OnValidator();
    }
}
