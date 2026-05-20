using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Dto;
using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Mappers;
using BlaisePascal.SmartHouse.Domain.LuminousDevices.Repository;
using BlaisePascal.SmartHouse.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Queries.GetAll
{
    public sealed class GetAllLampsQueryHandler : IRequestHandler<GetAllLampsQuery, Result<List<LampDto>>>
    {
        private readonly ILampRepository _lampRepository;
        public GetAllLampsQueryHandler(ILampRepository lampRepository)
        {
            _lampRepository = lampRepository;
        }
        public Task<Result<List<LampDto>>> Handle(GetAllLampsQuery request, CancellationToken cancellationToken)
        {
            var lamps = _lampRepository.GetAll();
            var lampDtos = new List<LampDto>();
            if (lamps == null)
                return Task.FromResult(Result.Failure<List<LampDto>>(Error.NullValue));
            foreach (var lamp in lamps)
            {
                lampDtos.Add(LampMapper.ToDto(lamp));
            }
            return Task.FromResult(Result.Success(lampDtos));
        }
    }
}
