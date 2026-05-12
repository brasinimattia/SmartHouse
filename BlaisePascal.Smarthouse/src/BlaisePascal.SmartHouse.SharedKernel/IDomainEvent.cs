using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.SharedKernel
{
    public interface IDomainEvent
    {
        DateTime OccurredOnUtc { get; }
    }

    public abstract class DomainEvent : IDomainEvent
    {
        public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
    }
}
