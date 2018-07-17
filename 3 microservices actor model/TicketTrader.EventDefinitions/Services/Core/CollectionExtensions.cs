using System.Collections.Generic;

namespace TicketTrader.EventDefinitions.Services.Core
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> @this, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                @this.Add(item);
            }
        }
    }
}
