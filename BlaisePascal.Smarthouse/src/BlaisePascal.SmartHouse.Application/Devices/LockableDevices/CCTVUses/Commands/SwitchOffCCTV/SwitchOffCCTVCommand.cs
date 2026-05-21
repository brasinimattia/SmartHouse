using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice;
using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.SwitchOffCCTV
{
    public sealed record SwitchOffCCTVCommand(Guid Id) : IRequest<Result<Guid>>;
    //public class SwitchOffCCTVCommand
    //{
    //    private readonly ICCTVRepository _cctvRepository;

    //    public SwitchOffCCTVCommand(ICCTVRepository cctvRepsotitory)
    //    {
    //        _cctvRepository = cctvRepsotitory;
    //    }

    //    public void Execute(Guid id)
    //    {
    //        CCTV cctv = _cctvRepository.GetById(id);
    //        if (cctv != null)
    //        {
    //            cctv.SwitchOff();
    //            _cctvRepository.Update(cctv);
    //        }
    //    }
    //}
}
