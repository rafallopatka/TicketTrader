using System;

namespace TicketTrader.Shared.Base.Infrastructure
{
    public static class CurrentDateTime
    {
        public static ICurrentDateTimeProvider Provider { private get; set; }

        static CurrentDateTime()
        {
            Provider = new CurrentDateTimeProvider();
        }

        public static DateTime Local => Provider.Local;
        public static DateTime Utc => Provider.Utc;
    }
}
