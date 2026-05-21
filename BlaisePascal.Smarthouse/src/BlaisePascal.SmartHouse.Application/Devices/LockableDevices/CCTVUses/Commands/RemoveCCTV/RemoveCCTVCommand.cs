using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice;
using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.RemoveCCTV
{
    public sealed record RemoveCCTVCommand(Guid Id) : IRequest<Result<Guid>>;
    //public class RemoveCCTVCommand
    //{
    //    private readonly ICCTVRepository _cctvRepository;

    //    public RemoveCCTVCommand(ICCTVRepository cctvRepository)
    //    {
    //        _cctvRepository = cctvRepository;
    //    }

    //    public void Execute(Guid id)
    //    {
    //        _cctvRepository.Remove(id);
    //    }
    //}
}
