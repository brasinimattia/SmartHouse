using BlaisePascal.SmartHouse.Domain.LockableDevices;
using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.AddCCTV
{
    public class AddCCTVCommandHandler : IRequestHandler<AddCCTVCommand, Result<Guid>>
    {
        public readonly ICCTVRepository _cctvRepository;

        public AddCCTVCommandHandler(ICCTVRepository cctvRepository)
        {
            _cctvRepository = cctvRepository;
        }

        public Task<Result<Guid>> Handle(AddCCTVCommand request, CancellationToken cancellationToken)
        {

            var cctv = new CCTV(request.CCTVName);
            var result = _cctvRepository.Add(cctv);

            if (result.IsFailure)
                return Task.FromResult(Result.Failure<Guid>(result.Error));

            return Task.FromResult(Result.Success<Guid>(cctv.Id));
        }
    }
}
