using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Dto;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Mapper;
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
    public sealed class GetDoorByIdHandler: IRequestHandler<GetDoorByIdQuery, Result<DoorDto>>
    {
        private readonly IDoorRepository _doorRepository;

        public GetDoorByIdHandler(IDoorRepository doorRepository)
        {
            _doorRepository = doorRepository;
        }

        public Task<Result<DoorDto>> Handle(GetDoorByIdQuery request, CancellationToken cancellationToken)
        {
            var door = _doorRepository.GetById(request.id);
            if (door == null)
                return Task.FromResult(Result.Failure<DoorDto>(Error.NullValue));
            
            return Task.FromResult(Result.Success<DoorDto>(DoorMapper.ToDto(door)));
        }
    }
}
