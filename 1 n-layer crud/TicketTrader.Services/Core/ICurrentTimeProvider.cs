using System;

namespace TicketTrader.Services.Core
{
    public interface ICurrentTimeProvider
    {
        DateTime Local { get; }
        DateTime Utc { get; }
    }
}
