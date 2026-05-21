using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice;
using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using BlaisePascal.SmartHouse.SharedKernel;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Command.AddDoor
{
   /* public class AddDoorCommand
    {
        private readonly IDoorRepository _doorRepository;

        public AddDoorCommand(IDoorRepository doorRepository)
        {
            _doorRepository = doorRepository;
        }

        public void Execute(string doorName)
        {
            _doorRepository.Add(new Door(doorName));
        }
    }*/

    public sealed record AddDoorCommand(string doorName): IRequest<Result<Guid>>;
}
