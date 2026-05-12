using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.SharedKernel;

namespace BlaisePascal.SmartHouse.Domain.LuminousDevices
{
    public interface ILuminous
    {
        Result IncreaseBrightness();
        Result DecreaseBrightness();
        Result ChangeBrightness(int brightness);
    }
}
