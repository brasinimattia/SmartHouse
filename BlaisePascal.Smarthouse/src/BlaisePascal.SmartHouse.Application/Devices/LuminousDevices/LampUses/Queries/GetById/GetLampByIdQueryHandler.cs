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

namespace BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Queries.GetById
{
    public sealed class GetLampByIdQueryHandler : IRequestHandler<GetLampByIdQuery, Result<LampDto>>
    {
        private readonly ILampRepository _lampRepository;
        public GetLampByIdQueryHandler(ILampRepository lampRepository)
        {
            _lampRepository = lampRepository;
        }
        public Task<Result<LampDto>> Handle(GetLampByIdQuery request, CancellationToken cancellationToken)
        {
            var lamp = _lampRepository.GetById(request.Id);
            if (lamp == null)
                return Task.FromResult(Result.Failure<LampDto>(Error.NullValue));
            return Task.FromResult(Result.Success<LampDto>(LampMapper.ToDto(lamp)));
        }
    }
}
