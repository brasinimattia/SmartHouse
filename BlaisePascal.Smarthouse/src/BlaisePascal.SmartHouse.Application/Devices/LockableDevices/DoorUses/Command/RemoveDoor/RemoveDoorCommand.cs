using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Command.RemoveDoor
{
    /*public class RemoveDoorCommand
    {
        private readonly IDoorRepository _doorRepository;

        public RemoveDoorCommand(IDoorRepository doorRepository)
        {
            _doorRepository = doorRepository;
        }

        public void Execute(Guid id)
        {
            _doorRepository.Remove(id);
        }
    }*/

    public sealed record RemoveDoorCommand(Guid id): IRequest<Result<Guid>>;
}
