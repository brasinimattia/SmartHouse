using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice;
using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.SetPasswordCCTV
{
    public sealed record SetPasswordCCTVCommand(Guid Id, string key) : IRequest<Result<Guid>>;

    //public class SetPasswordCCTVCommand
    //{
    //    private readonly ICCTVRepository _cctvRepository;

    //    public SetPasswordCCTVCommand(ICCTVRepository cctvRepsotitory)
    //    {
    //        _cctvRepository = cctvRepsotitory;
    //    }

    //    public void Execute(Guid id, string password)
    //    {
    //        CCTV cctv = _cctvRepository.GetById(id);
    //        if (cctv != null)
    //        {
    //            cctv.SetPassword(password);
    //            _cctvRepository.Update(cctv);
    //        }
    //    }
    //}
}
