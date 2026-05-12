using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.SharedKernel;

namespace BlaisePascal.SmartHouse.Domain.abstraction.Events
{
    public sealed class DeviceSwitchedOffEvent : DomainEvent
    {
        public Guid DeviceId { get; }

        public DeviceSwitchedOffEvent(Guid deviceId)
        {
            DeviceId = deviceId;
        }
    }
}
