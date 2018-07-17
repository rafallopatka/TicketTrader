using System;

namespace TicketTrader.Shared.Base.Infrastructure
{
    public interface ICurrentDateTimeProvider
    {
        DateTime Local { get; }
        DateTime Utc { get; }
    }
}
