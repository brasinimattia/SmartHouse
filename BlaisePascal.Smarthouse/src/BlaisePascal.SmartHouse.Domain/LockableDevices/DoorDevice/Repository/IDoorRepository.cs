using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice;
using BlaisePascal.SmartHouse.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice.Repository
{
    public interface IDoorRepository
    {
        Result Add(Door door);
        Result Update(Door door);
        Result Remove(Guid id);
        Result<Door> GetById(Guid id);
        Result<List<Door>> GetAll();
    }
}
