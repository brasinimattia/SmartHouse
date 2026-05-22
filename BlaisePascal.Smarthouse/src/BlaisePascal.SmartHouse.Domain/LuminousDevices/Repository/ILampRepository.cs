using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice;
using BlaisePascal.SmartHouse.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.LuminousDevices.Repository
{
    public interface ILampRepository
    {
        Result Add(Lamp lamp);
        Result Update(Lamp lamp);
        Result Remove(Guid id);
        Result<Lamp> GetById(Guid id);
        Result<List<Lamp>> GetAll();
    }
}
