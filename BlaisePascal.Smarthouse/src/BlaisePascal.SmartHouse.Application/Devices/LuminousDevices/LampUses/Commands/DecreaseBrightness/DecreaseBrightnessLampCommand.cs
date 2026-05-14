using BlaisePascal.SmartHouse.Domain.LuminousDevices;
using BlaisePascal.SmartHouse.Domain.LuminousDevices.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Commands.DecreaseBrightness
{
    public class DecreaseBrightnessLampCommand
    {
        private readonly ILampRepository _lampRepository;

        public DecreaseBrightnessLampCommand(ILampRepository lampRepository)
        {
            _lampRepository = lampRepository;
        }

        public void Execute(Guid id)
        {
            Lamp lamp = _lampRepository.GetById(id);
            if (lamp != null)
            {
                lamp.DecreaseBrightness();
                _lampRepository.Update(lamp);
            }
        }
    }
}
