using System;

namespace TicketTrader.Shared.Base.DDD
{
    public class AggregateRoot : DomainEntity
    {
        public DateTime Version { get; }

        protected EventBus EventBus => EventBus.Current;

        public AggregateRoot()
        {
            Version = DateTime.UtcNow;
        }
    }
}
