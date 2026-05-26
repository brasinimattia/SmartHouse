using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Dto;
using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Mappers;
using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice;
using BlaisePascal.SmartHouse.Domain.LockableDevices.CctvDevice.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Queries.GetCCTVById
{
    /*public class GetCCTVByIdQuery
    {
        private readonly ICCTVRepository _cctvRepository;

        public GetCCTVByIdQuery(ICCTVRepository cctvRepository)
        {
            _cctvRepository = cctvRepository;
        }

        public CCTVDto Execute(Guid id)
        {
            return CCTVMapper.ToDto(_cctvRepository.GetById(id));
        }
    }*/

    public sealed record GetCCTVByIdQuery(Guid id): IRequest<Result<CCTVDto>>;
    
}
