using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice;
using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Command.UnlockDoor
{
    public class UnlockDoorCommand
    {
        private readonly IDoorRepository _doorRepository;

        public UnlockDoorCommand(IDoorRepository doorRepository)
        {
            _doorRepository = doorRepository;
        }

        public void Execute(Guid id, string key)
        {
            Door door = _doorRepository.GetById(id);
            if (door != null)
            {
                door.Unlock(key);
                _doorRepository.Update(door);
            }
        }
    }
}
