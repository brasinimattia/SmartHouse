using BlaisePascal.SmartHouse.Domain.LuminousDevices.Repository;
using BlaisePascal.SmartHouse.Domain.LuminousDevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Dto;
using BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Mappers;
using MediatR;
using BlaisePascal.SmartHouse.SharedKernel;

namespace BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Queries.GetAll
{
    /*public class GetAllLampsQuery
    {
        private readonly ILampRepository _lampRepository;

        public GetAllLampsQuery(ILampRepository lampRepository)
        {
            _lampRepository = lampRepository;
        }

        public List<LampDto> Execute()
        {
            List<LampDto> result = new List<LampDto>();
            foreach(Lamp l in _lampRepository.GetAll())
            {
                result.Add(LampMapper.ToDto(l));
            }

            return result;
        }
    }*/

    public sealed record GetAllLampsQuery() : IRequest<Result<List<LampDto>>>;
}
