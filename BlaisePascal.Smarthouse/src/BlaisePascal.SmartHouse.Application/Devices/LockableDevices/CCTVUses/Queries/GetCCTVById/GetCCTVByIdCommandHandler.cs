using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Dto;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Mappers;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Dto;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.DoorUses.Mapper;
using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice.Repository;
using BlaisePascal.SmartHouse.Domain.LockableDevices.DoorDevice.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Queries.GetCCTVById
{
    public sealed class GetCCTVByIdQueryHandler: IRequestHandler<GetCCTVByIdQuery, Result<CCTVDto>>
    {
        private readonly ICCTVRepository _cctvRepository;
        public GetCCTVByIdQueryHandler(ICCTVRepository cctvRepository)
        {
            _cctvRepository = cctvRepository;
        }
        public Task<Result<CCTVDto>> Handle(GetCCTVByIdQuery request, CancellationToken cancellationToken)
        {
            var cctv = _cctvRepository.GetById(request.id);
            if (cctv == null)
                return Task.FromResult(Result.Failure<CCTVDto>(Error.NullValue));

            return Task.FromResult(Result.Success<CCTVDto>(CCTVMapper.ToDto(cctv.Value)));
        }
    }
}
