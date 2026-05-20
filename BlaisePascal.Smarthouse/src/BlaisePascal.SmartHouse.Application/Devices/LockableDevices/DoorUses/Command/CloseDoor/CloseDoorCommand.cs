using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Command.CloseDoor
{
    public class CloseDoorCommand
    {
        private readonly IDoorRepository _doorRepository;

        public CloseDoorCommand(IDoorRepository doorRepository)
        {
            _doorRepository = doorRepository;
        }

        public void Execute(Guid id)
        {
            var door = _doorRepository.GetById(id);
            if (door != null)
            {
                door.Close();
                _doorRepository.Update(door);
            }
        }
    }
}

