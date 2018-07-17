using System.Collections.Generic;

namespace TicketTrader.EventDefinitions.Services.Core
{
    public class PagedList<T> : List<T>
    {
        public PagedList(IEnumerable<T> collection, int skipped, int totalCount) : base(collection)
        {
            Skipped = skipped;
            TotalCount = totalCount;
        }

        public int Skipped { get; }
        public int TotalCount { get; }
    }
}