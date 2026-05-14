using BlaisePascal.SmartHouse.Domain.LuminousDevices.Repository;
using BlaisePascal.SmartHouse.Domain.LuminousDevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Commands.SwitchOff
{
    public class SwitchOffLampCommand
    {
        private readonly ILampRepository _lampRepository;

        public SwitchOffLampCommand(ILampRepository lampRepository)
        {
            _lampRepository = lampRepository;
        }

        public void Execute(Guid id)
        {
            Lamp lamp = _lampRepository.GetById(id);
            if(lamp != null)
            {
                lamp.SwitchOff();
                _lampRepository.Update(lamp);
            }
        }
    }
}
