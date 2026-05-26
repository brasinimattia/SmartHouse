using BlaisePascal.SmartHouse.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Device
{
    public interface ISwitchable
    {
        Result SwitchOn();
        Result SwitchOff();
        Result Toggle();
        void OnValidator();
    }
}
