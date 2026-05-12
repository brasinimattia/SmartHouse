using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.SharedKernel;

namespace BlaisePascal.SmartHouse.Domain.abstraction.Events
{
    public sealed class DeviceSwitchedOnEvent : DomainEvent
    {
        public Guid DeviceId { get; }

        public DeviceSwitchedOnEvent(Guid deviceId)
        {
            DeviceId = deviceId;
        }
    }
}
