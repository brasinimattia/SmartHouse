using BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Commands.AddCCTV;
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

namespace BlaisePascal.SmartHouse.Application.Devices.LockableDevices.CCTVUses.Queries.GetAllCCTV
{
    public sealed class GetAllCCTVQueryHandler : IRequestHandler<GetAllCCTVQuery, Result<List<CCTVDto>>>
    {
        private readonly ICCTVRepository _cctvRepository;

        public GetAllCCTVQueryHandler(ICCTVRepository cctvRepository)
        {
            _cctvRepository = cctvRepository;
        }

        public Task<Result<List<CCTVDto>>> Handle(GetAllCCTVQuery request, CancellationToken cancellationToken)
        {
            var cctvs = _cctvRepository.GetAll().Value;
            var cctvdtos = new List<CCTVDto>();
            if(cctvs == null)
                return Task.FromResult(Result.Failure<List<CCTVDto>>(Error.NullValue));
            foreach (var cctv in cctvs)
            {
                cctvdtos.Add(CCTVMapper.ToDto(cctv));
            }
            return Task.FromResult(Result.Success(cctvdtos));

        }
    }
}
