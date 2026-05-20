using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Dto;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Mapper;
using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice;
using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Queries.GetAllDoor
{
    public class GetAllDoorQuery
    {
        private readonly IDoorRepository _doorRepository;
    
        public GetAllDoorQuery(IDoorRepository doorRepository)
        {
            _doorRepository = doorRepository;
        }

        public List<DoorDto> Execute()
        {
            List<DoorDto> result = new List<DoorDto>();

            foreach (Door d in _doorRepository.GetAll())
            {
                if (d != null) 
                {
                    result.Add(DoorMapper.ToDto(d));
                }
            }

            return result;
        }
    }
}
