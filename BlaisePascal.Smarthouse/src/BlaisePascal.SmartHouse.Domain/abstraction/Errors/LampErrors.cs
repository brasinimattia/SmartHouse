using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.SharedKernel;

namespace BlaisePascal.SmartHouse.Domain.abstraction.Errors
{
    public record LampErrors: Error
    {
        public static readonly Error AlreadyOn = new("Lamp.AlreadyOn", "The lamp is already on.", ErrorType.Conflict);
        public static readonly Error AlreadyOff = new("Lamp.AlreadyOff", "The lamp is already off.", ErrorType.Conflict);
        public static readonly Error IsOff = new("Lamp.IsOff", "The lamp is off.", ErrorType.Conflict);

        public LampErrors(string code, string description, ErrorType type) : base(code, description, type)
        {
        }
    }
}
