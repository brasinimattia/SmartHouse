using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.AddCCTV;
using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice;
using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Queries.GetAllCCTV
{
    //public sealed class GetAllCCTVCommandHandler : IRequestHandler<GetAllCCTVCommand, Result<Guid>>
    //{
    //    private readonly ICCTVRepository _cctvRepository;

    //    public GetAllCCTVCommandHandler(ICCTVRepository cctvRepository)
    //    {
    //        _cctvRepository = cctvRepository;
    //    }

    //    public Task<Result<Guid>> Handle(AddCCTVCommand request, CancellationToken cancellationToken)
    //    {

    //        var cctv = new CCTV(request.CCTVName);
    //        var result = _cctvRepository.GetAll();

    //        if (result.IsFailure)
    //            return Task.FromResult(Result.Failure<Guid>(result.Error));

    //        return Task.FromResult(Result.Success<Guid>(cctv.Id));
    //    }
    //}
}
