using System;

namespace TicketTrader.Shared.Base.Infrastructure
{
    public class CurrentDateTimeProvider : ICurrentDateTimeProvider
    {
        public DateTime Local => DateTime.Now;
        public DateTime Utc => DateTime.UtcNow;
    }
}