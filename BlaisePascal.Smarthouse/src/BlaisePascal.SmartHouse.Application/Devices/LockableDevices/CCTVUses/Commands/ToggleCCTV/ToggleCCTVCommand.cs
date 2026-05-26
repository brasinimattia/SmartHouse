using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice;
using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.ToggleCCTV
{
    /*public class ToggleCCTVCommand
    {
        private readonly ICCTVRepository _cctvRepository;

        public ToggleCCTVCommand(ICCTVRepository cctvRepsotitory)
        {
            _cctvRepository = cctvRepsotitory;
        }

        public void Execute(Guid id)
        {
            CCTV cctv = _cctvRepository.GetById(id).Value;
            if(cctv != null)
            {
                cctv.Toggle();
                _cctvRepository.Update(cctv);
            }
        }
    }*/

    public sealed record ToggleCCTVCommand(Guid Id) : IRequest<Result<Guid>>;
}
