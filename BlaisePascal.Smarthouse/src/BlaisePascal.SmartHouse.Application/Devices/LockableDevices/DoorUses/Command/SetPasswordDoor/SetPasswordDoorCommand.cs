using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Dto;
using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Command.SetPasswordDoor
{
    /*public class SetPasswordDoorCommand
    {
        private readonly IDoorRepository _doorRepository;

        public SetPasswordDoorCommand(IDoorRepository doorRepository)
        {
            _doorRepository = doorRepository;
        }

        public void Execute(Guid id, string newPassword)
        {
            var door = _doorRepository.GetById(id);
            if (door != null)
            {
                door.SetPassword(newPassword);
                _doorRepository.Update(door);
            }
        }
    }*/

    public sealed record SetPasswordDoorCommand(Guid id, string newPassword): IRequest<Result<Guid>>;
}
