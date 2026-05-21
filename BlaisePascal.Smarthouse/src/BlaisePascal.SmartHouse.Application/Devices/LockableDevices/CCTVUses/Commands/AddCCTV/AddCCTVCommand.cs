using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice;
using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.AddCCTV
{
    public sealed record AddCCTVCommand(string CCTVName) : IRequest<Result<Guid>>;

    //public class AddCCTVCommand
    //{
    //    private readonly ICCTVRepository _cctvRepository;

    //    public AddCCTVCommand(ICCTVRepository cctvRepository)
    //    {
    //        _cctvRepository = cctvRepository;
    //    }

    //    public void Execute(string name)
    //    {
    //        _cctvRepository.Add(new CCTV(name));
    //    }
    //}
}
