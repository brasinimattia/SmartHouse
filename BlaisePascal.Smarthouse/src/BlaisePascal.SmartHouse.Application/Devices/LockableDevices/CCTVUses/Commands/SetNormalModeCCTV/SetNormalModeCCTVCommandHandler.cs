using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.SetNormalModeCCTV
{
    public sealed class SetNormalModeCCTVCommandHandler: IRequestHandler<SetNormalModeCCTVCommand, Result<Guid>>
    {
        private readonly ICCTVRepository _cctvRepository;

        public SetNormalModeCCTVCommandHandler(ICCTVRepository cctvRepository)
        {
            _cctvRepository = cctvRepository;
        }

        public Task<Result<Guid>> Handle(SetNormalModeCCTVCommand request, CancellationToken cancellationToken)
        {
            var cctv = _cctvRepository.GetById(request.Id).Value;
            if (cctv == null)
                return Task.FromResult(Result.Failure<Guid>(Error.NullValue));

            var result = cctv.SetNormalMode();
            if (result.IsFailure)
                return Task.FromResult(Result.Failure<Guid>(result.Error));

            _cctvRepository.Update(cctv);
            return Task.FromResult(Result.Success<Guid>(request.Id));
        }
    }
}
