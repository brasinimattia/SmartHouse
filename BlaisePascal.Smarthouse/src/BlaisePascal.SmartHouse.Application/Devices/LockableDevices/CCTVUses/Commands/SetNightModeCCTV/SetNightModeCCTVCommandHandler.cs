using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.lockCCTV;
using BlaisePascal.SmartHouse.Domain.abstraction.Errors;
using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.DetNightModeCCTV
{
    //public sealed class SetNightModeCCTVCommandHandler : IRequestHandler<SetNightModeCCTVCommand, Result<Guid>>
    //{
    //    //private readonly ICCTVRepository _cctvRepository;
    //    //public SetNightModeCCTVCommandHandler(ICCTVRepository cctvRepository)
    //    //{
    //    //    _cctvRepository = cctvRepository;
    //    //}
    //    //public Task<Result<Guid>> Handle(SetNightModeCCTVCommand request, CancellationToken cancellationToken)
    //    //{
    //    //    var cctv = _cctvRepository.GetById(request.Id);
    //    //    if (cctv == null)
    //    //        return Task.FromResult(Result.Failure<Guid>(Error.NullValue));
    //    //    var result = cctv.SetNightMode;
    //    //    if (result.IsFailure)
    //    //    {
    //    //        return Task.FromResult(Result.Failure<Guid>(result.Error));
    //    //    }
    //    //    _cctvRepository.Update(cctv);
    //    //    return Task.FromResult(Result.Success<Guid>(cctv.Id));
    //    //}
    //}
}
