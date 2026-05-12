using BlaisePascal.SmartHouse.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice
{
    public interface ICCTV
    {
        Result SetNightMode();
        Result SetNormalMode();
        Result SetMode(CCTVMode mode);
        Result StartRecording();
        Result StopRecording();
    }
}
