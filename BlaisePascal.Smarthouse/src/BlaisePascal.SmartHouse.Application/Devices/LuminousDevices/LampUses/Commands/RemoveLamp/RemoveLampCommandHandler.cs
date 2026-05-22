using BlaisePascal.SmartHouse.Domain.LuminousDevices.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Commands.RemoveLamp
{
    public sealed class RemoveLampCommandHandler: IRequestHandler<RemoveLampCommand, Result<Guid>>
    {
        private readonly ILampRepository _lampRepository;
        public RemoveLampCommandHandler(ILampRepository lampRepository)
        {
            _lampRepository = lampRepository;
        }
        public Task<Result<Guid>> Handle(RemoveLampCommand request, CancellationToken cancellationToken)
        {
            var lamp = _lampRepository.GetById(request.Id).Value;
            if (lamp == null)
                return Task.FromResult(Result.Failure<Guid>(Error.NullValue));
            
            var result = _lampRepository.Remove(request.Id);
            if (result.IsFailure)
            {
                return Task.FromResult(Result.Failure<Guid>(result.Error));
            }
            return Task.FromResult(Result.Success<Guid>(request.Id));
        }
    }
}
