using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Dto;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Mapper;
using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Dto;
using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Mappers;
using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice.Repository;
using BlaisePascal.SmartHouse.Domain.LuminousDevices;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Queries.GetAllDoor
{
    public sealed class GetAllDoorQueryHandler : IRequestHandler<GetAllDoorQuery, Result<List<DoorDto>>>
    {
        private readonly IDoorRepository _doorRepository;
        public GetAllDoorQueryHandler(IDoorRepository doorRepository)
        {
            _doorRepository = doorRepository;
        }
        public Task<Result<List<DoorDto>>> Handle(GetAllDoorQuery request, CancellationToken cancellationToken)
        {
            var doors = _doorRepository.GetAll();
            var doorDtos = new List<DoorDto>();
            if (doors == null)
                return Task.FromResult(Result.Failure<List<DoorDto>>(Error.NullValue));
            foreach (var door in doors)
            {
                doorDtos.Add(DoorMapper.ToDto(door));
            }

            return Task.FromResult(Result.Success(doorDtos));
        }
    }
}
