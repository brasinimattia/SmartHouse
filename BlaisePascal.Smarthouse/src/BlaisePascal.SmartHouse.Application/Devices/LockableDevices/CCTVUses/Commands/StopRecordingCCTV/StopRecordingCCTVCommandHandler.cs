using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.StopRecordingCCTV
{
    public sealed class StopRecordingCCTVCommandHandler : IRequestHandler<StopRecordingCCTVCommand, Result<Guid>>
    {
        private readonly ICCTVRepository _cctvRepository;
        public StopRecordingCCTVCommandHandler(ICCTVRepository cctvRepository)
        {
            _cctvRepository = cctvRepository;
        }
        public Task<Result<Guid>> Handle(StopRecordingCCTVCommand request, CancellationToken cancellationToken)
        {
            var cctv = _cctvRepository.GetById(request.Id).Value;
            if (cctv == null)
                return Task.FromResult(Result.Failure<Guid>(Error.NullValue));
            var result = cctv.StopRecording();
            if (result.IsFailure)
            {
                return Task.FromResult(Result.Failure<Guid>(result.Error));
            }
            _cctvRepository.Update(cctv);
            return Task.FromResult(Result.Success<Guid>(cctv.Id));
        }
    }
}
