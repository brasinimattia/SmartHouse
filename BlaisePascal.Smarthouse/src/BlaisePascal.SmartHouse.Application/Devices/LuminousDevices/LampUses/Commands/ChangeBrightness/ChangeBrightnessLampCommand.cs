using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.LuminousDevices;
using BlaisePascal.SmartHouse.Domain.LuminousDevices.Repository;

namespace BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Commands.ChangeBrightness
{
    public class ChangeBrightnessLampCommand
    {
        private readonly ILampRepository _lampRepository;

        public ChangeBrightnessLampCommand(ILampRepository lampRepository)
        {
            _lampRepository = lampRepository;
        }

        public void Execute(Guid id, int amount)
        {
            Lamp lamp = _lampRepository.GetById(id);
            if(lamp != null)
            {
                lamp.ChangeBrightness(amount);
                _lampRepository.Update(lamp);
            }
        }
    }
}
