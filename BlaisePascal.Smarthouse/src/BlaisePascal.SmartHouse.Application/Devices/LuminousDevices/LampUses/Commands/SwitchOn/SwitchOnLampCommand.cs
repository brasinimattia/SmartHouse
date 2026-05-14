using BlaisePascal.SmartHouse.Domain.LuminousDevices.Repository;
using BlaisePascal.SmartHouse.Domain.LuminousDevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Commands.SwitchOn
{
    public class SwitchOnLampCommand
    {
        private readonly ILampRepository _lampRepository;

        public SwitchOnLampCommand(ILampRepository lampRepository)
        {
            _lampRepository = lampRepository;
        }

        public void Execute(Guid id)
        {
            Lamp lamp = _lampRepository.GetById(id);
            if(lamp != null)
            {
                lamp.SwitchOn();
                _lampRepository.Update(lamp);
            }

        }
    }
}
