using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Commands.SwitchOff;
using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice.Repository;
using BlaisePascal.SmartHouse.Domain.LuminousDevices.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.SwitchOffCCTV
{
    public sealed class SwitchOffCCTVCommandHandler : IRequestHandler<SwitchOffCCTVCommand, Result<Guid>>
    {
        private readonly ICCTVRepository _cctvRepository;
        public SwitchOffCCTVCommandHandler(ICCTVRepository cctvRepository)
        {
            _cctvRepository = cctvRepository;
        }
        public Task<Result<Guid>> Handle(SwitchOffCCTVCommand request, CancellationToken cancellationToken)
        {
            var lamp = _cctvRepository.GetById(request.Id).Value;
            if (lamp == null)
                return Task.FromResult(Result.Failure<Guid>(Error.NullValue));
            var result = lamp.SwitchOff();
            if (result.IsFailure)
            {
                return Task.FromResult(Result.Failure<Guid>(result.Error));
            }
            _cctvRepository.Update(lamp);
            return Task.FromResult(Result.Success<Guid>(lamp.Id));
        }
    }
}
