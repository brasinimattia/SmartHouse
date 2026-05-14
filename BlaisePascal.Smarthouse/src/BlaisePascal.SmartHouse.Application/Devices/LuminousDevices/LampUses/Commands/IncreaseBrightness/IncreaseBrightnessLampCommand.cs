using BlaisePascal.SmartHouse.Domain.LuminousDevices;
using BlaisePascal.SmartHouse.Domain.LuminousDevices.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Commands
{
    public class IncreaseBrightnessLampCommand
    {
        private readonly ILampRepository _lampRepository;

        public IncreaseBrightnessLampCommand(ILampRepository lampRepository)
        {
            _lampRepository = lampRepository;
        }

        public void Execute(Guid id)
        {
            Lamp lamp = _lampRepository.GetById(id);
            if(lamp != null)
            {
                lamp.IncreaseBrightness();
                _lampRepository.Update(lamp);
            }
        }
    }
}
