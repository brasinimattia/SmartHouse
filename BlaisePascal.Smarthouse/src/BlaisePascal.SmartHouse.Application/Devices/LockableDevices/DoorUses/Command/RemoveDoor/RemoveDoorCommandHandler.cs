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
    public sealed class RemoveDoorCommandHandler : IRequestHandler<RemoveDoorCommand, Result<Guid>>
    {
        private readonly IDoorRepository _doorRepository;
        public RemoveDoorCommandHandler(IDoorRepository doorRepository)
        {
            _doorRepository = doorRepository;
        }
        public Task<Result<Guid>> Handle(RemoveDoorCommand request, CancellationToken cancellationToken)
        {
            var door = _doorRepository.GetById(request.id);
            if (door == null)
                return Task.FromResult(Result.Failure<Guid>(Error.NullValue));
            
            var result = _doorRepository.Remove(request.id);
            if(result.IsFailure)
                return Task.FromResult(Result.Failure<Guid>(result.Error));

            return Task.FromResult(Result.Success<Guid>(request.id));
        }
    }
}
