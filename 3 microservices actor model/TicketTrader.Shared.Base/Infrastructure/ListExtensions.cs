using System.Collections.Generic;

namespace TicketTrader.Shared.Base.Infrastructure
{
    public static class ListExtensions
    {
        public static void Replace<T>(this @IList<T> @this, T old, T current)
        {
            var index = @this.IndexOf(old);
            @this[index] = current;
        }
    }
}