using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice;
using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.SetNewNameCCTV
{
    public sealed record  SetNewNameCCTVCommand(Guid Id, string cctvNewName) : IRequest<Result<Guid>>;

    //public class SetNewNameCCTVCommand
    //{
    //    private readonly ICCTVRepository _cctvRepository;

    //    public SetNewNameCCTVCommand(ICCTVRepository cctvRepsotitory)
    //    {
    //        _cctvRepository = cctvRepsotitory;
    //    }

    //    public void Execute(Guid id, string name)
    //    {
    //        CCTV cctv = _cctvRepository.GetById(id);
    //        if (cctv != null)
    //        {
    //            cctv.SetNewName(name);
    //            _cctvRepository.Update(cctv);
    //        }
    //    }
    //}
}
