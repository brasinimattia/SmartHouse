using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice;
using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice.Repository;
using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice;
using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Command.SwitchOffDoor
{
    /*public class SwitchOffDoorCommand
    {
        private readonly IDoorRepository _doorRepository;

        public SwitchOffDoorCommand(IDoorRepository doorRepsotitory)
        {
            _doorRepository = doorRepsotitory;
        }

        public void Execute(Guid id)
        {
            Door door = _doorRepository.GetById(id);
            if (door != null)
            {
                door.SwitchOff();
                _doorRepository.Update(door);
            }
        }
    }*/

    public sealed record SwitchOffDoorCommand(Guid id): IRequest<Result<Guid>>;
}
