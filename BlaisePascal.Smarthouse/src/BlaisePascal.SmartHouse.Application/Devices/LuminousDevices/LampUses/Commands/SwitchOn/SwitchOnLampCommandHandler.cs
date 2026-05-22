using BlaisePascal.SmartHouse.Domain.LuminousDevices.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Commands.SwitchOn
{
    public sealed class SwitchOnLampCommandHandler : IRequestHandler<SwitchOnLampCommand, Result<Guid>>
    {
        private readonly ILampRepository _lampRepository;
        public SwitchOnLampCommandHandler(ILampRepository lampRepository)
        {
            _lampRepository = lampRepository;
        }
        public Task<Result<Guid>> Handle(SwitchOnLampCommand request, CancellationToken cancellationToken)
        {
            var lamp = _lampRepository.GetById(request.Id).Value;
            if (lamp == null)
                return Task.FromResult(Result.Failure<Guid>(Error.NullValue));
            var result = lamp.SwitchOn();
            if (result.IsFailure)
            {
                return Task.FromResult(Result.Failure<Guid>(result.Error));
            }
            _lampRepository.Update(lamp);
            return Task.FromResult(Result.Success<Guid>(lamp.Id));
        }
    }
}
