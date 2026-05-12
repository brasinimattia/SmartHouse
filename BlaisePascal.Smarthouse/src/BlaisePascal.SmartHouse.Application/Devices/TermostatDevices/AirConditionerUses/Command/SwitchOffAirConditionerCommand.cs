using BlaisePascal.SmartHouse.Domain.TemperatureDevices.AirConditionerDevice;
using BlaisePascal.SmartHouse.Domain.TemperatureDevices.AirConditionerDevice.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.TermostatDevices.AirConditionerUses.Command
{
    public class SwitchOffAirConditionerCommand
    {
        private readonly IAirConditionerRepository _repository;

        public SwitchOffAirConditionerCommand(IAirConditionerRepository repository)
        {
            _repository = repository;
        }

        public void Execute(Guid id)
        {
            AirConditioner cond = _repository.GetById(id);
            if(cond != null)
            {
                cond.SwitchOff();
                _repository.Update(cond);
            }
        }
    }
}
