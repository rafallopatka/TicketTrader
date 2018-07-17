using System;

namespace TicketTrader.Services.Core
{
    public class CurrentTimeProvider : ICurrentTimeProvider
    {
        public DateTime Local => DateTime.Now;
        public DateTime Utc => DateTime.UtcNow;
    }
}