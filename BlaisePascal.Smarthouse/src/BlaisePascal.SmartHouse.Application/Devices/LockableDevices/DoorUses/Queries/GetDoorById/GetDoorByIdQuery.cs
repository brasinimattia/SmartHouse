using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Dto;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Mapper;
using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice;
using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Queries.GetDoorById
{
    /*public class GetDoorByIdQuery
    {
        private readonly IDoorRepository _doorRepository;
        public GetDoorByIdQuery(IDoorRepository doorRepository)
        {
            _doorRepository = doorRepository;
        }
        public DoorDto Execute(Guid id)
        {
            return DoorMapper.ToDto(_doorRepository.GetById(id));
        }
    }*/

    public sealed record GetDoorByIdQuery(Guid id): IRequest<Result<DoorDto>>;
}
