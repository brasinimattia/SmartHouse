using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Command.SwitchOffDoor
{
    public sealed class SwitchOffDoorCommandHandler: IRequestHandler<SwitchOffDoorCommand, Result<Guid>>
    {
        private readonly IDoorRepository _doorRepository;

        public SwitchOffDoorCommandHandler(IDoorRepository doorRepository)
        {
            _doorRepository = doorRepository;
        }

        public Task<Result<Guid>> Handle(SwitchOffDoorCommand request, CancellationToken cancellationToken)
        {
            var door = _doorRepository.GetById(request.id);
            if (door == null)
                return Task.FromResult(Result.Failure<Guid>(Error.NullValue));

            var result = door.SwitchOff();
            if (result.IsFailure)
                return Task.FromResult(Result.Failure<Guid>(result.Error));

            _doorRepository.Update(door);
            return Task.FromResult(Result.Success<Guid>(door.Id));
        }
    }
}
