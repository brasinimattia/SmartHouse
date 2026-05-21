using BlaisePascal.SmartHouse.Domain.LockableDevices;
using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.RemoveCCTV
{
    public sealed class RemoveCCTVCommandHandler : IRequestHandler<RemoveCCTVCommand, Result<Guid>>
    {
        private readonly ICCTVRepository _cctvRepository;
        public RemoveCCTVCommandHandler(ICCTVRepository cctvRepository)
        {
            _cctvRepository = cctvRepository;
        }
        public Task<Result<Guid>> Handle(RemoveCCTVCommand request, CancellationToken cancellationToken)
        {
            var cctv = _cctvRepository.GetById(request.Id);
            if (cctv == null)
                return Task.FromResult(Result.Failure<Guid>(Error.NullValue));
            _cctvRepository.Remove(request.Id);
            return Task.FromResult(Result.Success<Guid>(request.Id));
        }
    }
}
