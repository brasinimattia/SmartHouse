using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.Device;
using BlaisePascal.SmartHouse.Domain.LuminousDevices.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;

namespace BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Commands.ChangeBrightness
{
    public sealed class ChangeBrightnessLampCommandHandler : IRequestHandler<ChangeBrightnessLampCommand, Result<Guid>>
    {
        private readonly ILampRepository _lampRepository;
        public ChangeBrightnessLampCommandHandler(ILampRepository lampRepository)
        {
            _lampRepository = lampRepository;
        }

        public Task<Result<Guid>> Handle(ChangeBrightnessLampCommand request, CancellationToken cancellationToken)
        {
            var lamp = _lampRepository.GetById(request.Id).Value;
            if (lamp == null)
                return Task.FromResult(Result.Failure<Guid>(Error.NullValue));
            if(lamp.Status == DeviceStatus.Off)
                return Task.FromResult(Result.Failure<Guid>(Error.Failure("The Lamp is off", "The Lamp is off")));
            var result = lamp.ChangeBrightness(request.Amount);
            if(result.IsFailure)
            {
                return Task.FromResult(Result.Failure<Guid>(result.Error));
            }
            _lampRepository.Update(lamp);
            return Task.FromResult(Result.Success<Guid>(lamp.Id));
        }
    }
}
