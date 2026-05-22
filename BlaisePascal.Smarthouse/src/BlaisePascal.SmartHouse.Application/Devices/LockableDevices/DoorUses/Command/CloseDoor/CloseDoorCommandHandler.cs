using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Command.CloseDoor
{
    public sealed class CloseDoorCommandHandler: IRequestHandler<CloseDoorCommand, Result<Guid>>
    {
        private readonly IDoorRepository _doorRepository;

        public CloseDoorCommandHandler(IDoorRepository doorRepository)
        {
            _doorRepository = doorRepository;
        }

        public Task<Result<Guid>> Handle(CloseDoorCommand request, CancellationToken cancellationToken)
        {
            var door = _doorRepository.GetById(request.id).Value;
            if (door == null)
                return Task.FromResult(Result.Failure<Guid>(Error.NullValue));
            
            var result = door.Close();
            if (result.IsFailure)
                return Task.FromResult(Result.Failure<Guid>(result.Error));
            
            return Task.FromResult(Result.Success<Guid>(door.Id));
        }
    }
}
