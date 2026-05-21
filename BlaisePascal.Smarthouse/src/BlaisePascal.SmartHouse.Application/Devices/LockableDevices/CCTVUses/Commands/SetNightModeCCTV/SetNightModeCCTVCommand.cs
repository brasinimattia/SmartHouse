using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice;
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
    public sealed record SetNightModeCCTVCommand(Guid Id) : IRequest<Result<Guid>>;
    //public class SetNightModeCCTVCommand
    //{
    //    private readonly ICCTVRepository _cctvRepository;

    //    public SetNightModeCCTVCommand(ICCTVRepository cctvRepsotitory)
    //    {
    //        _cctvRepository = cctvRepsotitory;
    //    }

    //    public void Execute(Guid id)
    //    {
    //        CCTV cctv = _cctvRepository.GetById(id);
    //        if (cctv != null)
    //        {
    //            cctv.SetNightMode();
    //            _cctvRepository.Update(cctv);
    //        }
    //    }
    //}
}
