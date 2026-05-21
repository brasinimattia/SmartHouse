using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice;
using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.SetModeCCTV
{

    public sealed record SetModeCCTVCommand(Guid Id, CCTVMode mode) : IRequest<Result<Guid>>;

    //public class SetModeCCTVCommand
    //{
    //    private readonly ICCTVRepository _cctvRepository;

    //    public SetModeCCTVCommand(ICCTVRepository cctvRepsotitory)
    //    {
    //        _cctvRepository = cctvRepsotitory;
    //    }

    //    public void Execute(Guid id, CCTVMode mode)
    //    {
    //        CCTV cctv = _cctvRepository.GetById(id);
    //        if (cctv != null)
    //        {
    //            cctv.SetMode(mode);
    //            _cctvRepository.Update(cctv);
    //        }
    //    }
    //}
}
