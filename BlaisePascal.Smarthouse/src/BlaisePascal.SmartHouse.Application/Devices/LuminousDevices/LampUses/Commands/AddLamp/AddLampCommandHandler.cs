using BlaisePascal.SmartHouse.Domain.LuminousDevices;
using BlaisePascal.SmartHouse.Domain.LuminousDevices.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Commands.AddLamp
{
    public sealed class AddLampCommandHandler: IRequestHandler<AddLampCommand, Result<Guid>>
    {
        private readonly ILampRepository _lampRepository;

        public AddLampCommandHandler(ILampRepository lampRepository)
        {
            _lampRepository = lampRepository;
        }

        public Task<Result<Guid>> Handle(AddLampCommand request, CancellationToken cancellationToken)
        {

            var lamp = new Lamp(request.LampName);
            var result = _lampRepository.Add(lamp);

            if(result.IsFailure)
                return Task.FromResult(Result.Failure<Guid>(result.Error));
            
            return Task.FromResult(Result.Success<Guid>(lamp.Id));
        }
    }
}
