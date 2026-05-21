using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice;
using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Command.AddDoor
{
    public sealed class AddDoorCommandHandler: IRequestHandler<AddDoorCommand, Result<Guid>>
    {
        private readonly IDoorRepository _doorRepository;            

        public AddDoorCommandHandler(IDoorRepository doorRepository)
        {
            _doorRepository = doorRepository;
        }

        public Task<Result<Guid>> Handle(AddDoorCommand request, CancellationToken cancellationToken)
        {
            var door = new Door(request.doorName);
            var result = _doorRepository.Add(door);
            if(result.IsFailure)
                return Task.FromResult(Result.Failure<Guid>(result.Error));
            
            return Task.FromResult(Result.Success<Guid>(door.Id));
        }
    }
}
