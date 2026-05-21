using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Command.LockDoor
{
    /*public class LockDoorCommand
    {
        private readonly IDoorRepository _doorRepository;

        public LockDoorCommand(IDoorRepository doorRepository)
        {
            _doorRepository = doorRepository;
        }

        public void Execute(Guid id, string key)
        {
            var door = _doorRepository.GetById(id);
            if (door != null)
            {
                door.Lock(key);
                _doorRepository.Update(door);
            }
        }
    }*/

    public sealed record LockDoorCommand(Guid id, string key): IRequest<Result<Guid>>;
}