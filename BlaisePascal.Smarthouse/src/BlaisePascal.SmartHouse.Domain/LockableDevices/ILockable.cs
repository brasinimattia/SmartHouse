using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.SharedKernel;

namespace BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice
{
    public interface ILockable
    {
        Result Lock(string key);
        Result Unlock(string key);
    }
}
