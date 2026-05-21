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
    public sealed class LockDoorCommandHandler : IRequestHandler<LockDoorCommand, Result<Guid>>
    {
        private readonly IDoorRepository _doorRepository;
        public LockDoorCommandHandler(IDoorRepository doorRepository)
        {
            _doorRepository = doorRepository;
        }
        public Task<Result<Guid>> Handle(LockDoorCommand request, CancellationToken cancellationToken)
        {
            var door = _doorRepository.GetById(request.id);
            if (door == null)
                return Task.FromResult(Result.Failure<Guid>(Error.NullValue));

            var result = door.Lock(request.key);
            if (result.IsFailure)
                return Task.FromResult(Result.Failure<Guid>(result.Error));

            _doorRepository.Update(door);
            return Task.FromResult(Result.Success<Guid>(door.Id));
        }
    }
}
