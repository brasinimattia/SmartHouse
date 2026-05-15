using BlaisePascal.SmartHouse.Domain.LuminousDevices.Repository;
using BlaisePascal.SmartHouse.Domain.LuminousDevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using BlaisePascal.SmartHouse.SharedKernel;

namespace BlaisePascal.SmartHouse.Application.Devices.LuminousDevices.LampUses.Commands.AddLamp
{
    public sealed record AddLampCommand(string LampName) : IRequest<Result<Guid>>;
}
