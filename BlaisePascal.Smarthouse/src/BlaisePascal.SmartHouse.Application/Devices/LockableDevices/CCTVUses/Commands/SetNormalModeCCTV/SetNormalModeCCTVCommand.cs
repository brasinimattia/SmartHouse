using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice;
using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.SetNormalModeCCTV
{
    /*public class SetNormalModeCCTVCommand
    {
        private readonly ICCTVRepository _cctvRepository;

        public SetNormalModeCCTVCommand(ICCTVRepository cctvRepsotitory)
        {
            _cctvRepository = cctvRepsotitory;
        }

        public void Execute(Guid id)
        {
            CCTV cctv = _cctvRepository.GetById(id);
            if (cctv != null)
            {
                cctv.SetNormalMode();
                _cctvRepository.Update(cctv);
            }
        }
    }*/

    public sealed record SetNormalModeCCTVCommand(Guid Id): IRequest<Result<Guid>>;
}
