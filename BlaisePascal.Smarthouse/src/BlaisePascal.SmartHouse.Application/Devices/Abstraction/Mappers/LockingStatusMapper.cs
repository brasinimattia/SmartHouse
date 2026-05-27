using BlaisePascal.SmartHouse.Domain.LockableDevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.Abstraction.Mappers
{
    public class LockingStatusMapper
    {
        public static string ToDto(LockingStatus status)
        {
            return status switch
            {
                LockingStatus.Unlocked => "UNLOCKED",
                LockingStatus.Locked => "LOCKED",
                _ => "UNKNOWN"
            };
        }

        public static LockingStatus ToDomain(string status)
        {
            return status.ToUpperInvariant() switch
            {
                "UNLOCKED" => LockingStatus.Unlocked,
                "LOCKED" => LockingStatus.Locked,
                "UNKNOWN" => LockingStatus.Unknown,
                _ => throw new ArgumentException($"Invalid status: {status}")
            };
        }
    }
}
