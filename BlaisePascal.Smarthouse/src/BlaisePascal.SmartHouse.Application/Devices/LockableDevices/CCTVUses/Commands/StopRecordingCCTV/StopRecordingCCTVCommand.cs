using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice;
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
    public sealed record StopRecordingCCTVCommand(Guid Id) : IRequest<Result<Guid>>;

    //public class StopRecordingCCTVCommand
    //{
    //    private readonly ICCTVRepository _cctvRepository;

    //    public StopRecordingCCTVCommand(ICCTVRepository cctvRepsotitory)
    //    {
    //        _cctvRepository = cctvRepsotitory;
    //    }

    //    public void Execute(Guid id)
    //    {
    //        CCTV cctv = _cctvRepository.GetById(id);
    //        if (cctv != null)
    //        {
    //            cctv.StopRecording();
    //            _cctvRepository.Update(cctv);
    //        }
    //    }
    //}
}
