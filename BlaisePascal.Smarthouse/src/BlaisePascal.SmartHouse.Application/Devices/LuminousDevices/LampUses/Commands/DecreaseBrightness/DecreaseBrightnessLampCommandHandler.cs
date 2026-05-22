using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Commands.DecreaseBrightness;
using BlaisePascal.SmartHouse.Domain.LuminousDevices.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;

namespace BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Commands
{
    public sealed class DecreaseBrightnessLampCommandHandler: IRequestHandler<DecreaseBrightnessLampCommand, Result<Guid>>
    {
        private readonly ILampRepository _lampRepository;
        public DecreaseBrightnessLampCommandHandler(ILampRepository lampRepository)
        {
            _lampRepository = lampRepository;
        }
        public Task<Result<Guid>> Handle(DecreaseBrightnessLampCommand request, CancellationToken cancellationToken)
        {
            var lamp = _lampRepository.GetById(request.Id).Value;
            if (lamp == null)
                return Task.FromResult(Result.Failure<Guid>(Error.NullValue));
            var result = lamp.DecreaseBrightness();
            if (result.IsFailure)
            {
                return Task.FromResult(Result.Failure<Guid>(result.Error));
            }
            _lampRepository.Update(lamp);
            return Task.FromResult(Result.Success<Guid>(lamp.Id));
        }

    }
}
