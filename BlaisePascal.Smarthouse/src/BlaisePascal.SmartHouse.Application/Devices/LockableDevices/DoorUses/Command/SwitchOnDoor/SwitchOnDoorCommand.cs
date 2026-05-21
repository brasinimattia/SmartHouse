using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice;
using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Command.SwitchOnDoor
{
    /*public class SwitchOnDoorCommand
    {
        private readonly IDoorRepository _doorRepository;

        public SwitchOnDoorCommand(IDoorRepository doorRepsotitory)
        {
            _doorRepository = doorRepsotitory;
        }

        public void Execute(Guid id)
        {
            Door door = _doorRepository.GetById(id);
            if (door != null)
            {
                door.SwitchOn();
                _doorRepository.Update(door);
            }
        }
    }*/

    public sealed record SwitchOnDoorCommand(Guid id): IRequest<Result<Guid>>;
}
