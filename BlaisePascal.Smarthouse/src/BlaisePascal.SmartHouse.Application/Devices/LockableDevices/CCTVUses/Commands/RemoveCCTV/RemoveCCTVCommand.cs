using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice;
using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.RemoveCCTV
{
    public class RemoveCCTVCommand
    {
        private readonly ICCTVRepository _cctvRepository;

        public RemoveCCTVCommand(ICCTVRepository cctvRepository)
        {
            _cctvRepository = cctvRepository;
        }

        public void Execute(Guid id)
        {
            _cctvRepository.Remove(id);
        }
    }
}
