using System.Collections.Generic;

namespace TicketTrader.Services.Core
{
    public static class PagedCollectionExtensions
    {
        public static PagedList<T> ToPagedCollection<T>(this IEnumerable<T> collection, int skipped, int totalCount)
        {
            return new PagedList<T>(collection, skipped, totalCount);
        }
    }
}