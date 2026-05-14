using BlaisePascal.SmartHouse.Domain.LuminousDevices.Repository;
using BlaisePascal.SmartHouse.Domain.LuminousDevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Commands.AddLamp
{
    public class AddLampCommand
    {
        private readonly ILampRepository _lampRepository;

        public AddLampCommand(ILampRepository lampRepository) 
        {
            _lampRepository = lampRepository;
        }

        public void Execute(string lampName)
        {
            _lampRepository.Add(new Lamp(lampName));
        }
    }
}
