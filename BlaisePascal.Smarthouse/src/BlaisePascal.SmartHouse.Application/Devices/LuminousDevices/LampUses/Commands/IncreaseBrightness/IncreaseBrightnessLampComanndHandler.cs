using BlaisePascal.SmartHouse.Domain.LuminousDevices.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Commands.IncreaseBrightness
{
    public sealed class IncreaseBrightnessLampComanndHandler : IRequestHandler<IncreaseBrightnessLampCommand, Result<Guid>>
    {
        private readonly ILampRepository _lampRepository;
        public IncreaseBrightnessLampComanndHandler(ILampRepository lampRepository)
        {
            _lampRepository = lampRepository;
        }
        public Task<Result<Guid>> Handle(IncreaseBrightnessLampCommand request, CancellationToken cancellationToken)
        {
            var lamp = _lampRepository.GetById(request.Id);
            if (lamp == null)
                return Task.FromResult(Result.Failure<Guid>(Error.NullValue));
            var result = lamp.IncreaseBrightness();
            if (result.IsFailure)
            {
                return Task.FromResult(Result.Failure<Guid>(result.Error));
            }
            _lampRepository.Update(lamp);
            return Task.FromResult(Result.Success<Guid>(lamp.Id));
        }
    }
}
