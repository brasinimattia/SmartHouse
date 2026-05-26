using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.ToggleCCTV
{
    public sealed class ToggleCCTVCommandHandler : IRequestHandler<ToggleCCTVCommand, Result<Guid>>
    {
        private readonly ICCTVRepository _cctvRepository;
        public ToggleCCTVCommandHandler(ICCTVRepository cctvRepsotitory)
        {
            _cctvRepository = cctvRepsotitory;
        }
        public Task<Result<Guid>> Handle(ToggleCCTVCommand request, CancellationToken cancellationToken)
        {
            var cctv = _cctvRepository.GetById(request.Id).Value;
            if (cctv == null)
                return Task.FromResult(Result.Failure<Guid>(Error.NullValue));
            var result = cctv.Toggle();
            if (result.IsFailure)
            {
                return Task.FromResult(Result.Failure<Guid>(result.Error));
            }
            _cctvRepository.Update(cctv);
            return Task.FromResult(Result.Success<Guid>(cctv.Id));
        }
    }
}
