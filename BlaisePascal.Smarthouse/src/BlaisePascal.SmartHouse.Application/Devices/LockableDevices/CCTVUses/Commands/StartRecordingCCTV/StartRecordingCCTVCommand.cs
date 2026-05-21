using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice;
using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.StartRecordingCCTV
{
    public sealed record StartRecordingCCTVCommand(Guid Id) : IRequest<Result<Guid>>;

    //public class StartRecordingCCTVCommand
    //{
    //    private readonly ICCTVRepository _cctvRepository;

    //    public StartRecordingCCTVCommand(ICCTVRepository cctvRepsotitory)
    //    {
    //        _cctvRepository = cctvRepsotitory;
    //    }

    //    public void Execute(Guid id)
    //    {
    //        CCTV cctv = _cctvRepository.GetById(id);
    //        if (cctv != null)
    //        {
    //            cctv.StartRecording();
    //            _cctvRepository.Update(cctv);
    //        }
    //    }
    //}
}
