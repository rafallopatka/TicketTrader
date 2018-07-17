using System;
using System.Collections.Generic;

namespace TicketTrader.Shared.Base.CQRS.Queries
{
    public class PaginatedResult<T>
    {
        public List<T> Items { get; private set; }
        public int PageSize { get; private set; }
        public int PageNumber { get; private set; }
        public int PagesCount { get; private set; }
        public int TotalItemsCount { get; private set; }

        public PaginatedResult(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Items = new List<T>();
            PagesCount = 0;
            TotalItemsCount = 0;
        }

        public PaginatedResult(List<T> items, int pageNumber, int pageSize, int totalItemsCount)
        {
            Items = items;
            PageNumber = pageNumber;
            PageSize = pageSize;
            PagesCount = countPages(pageSize, totalItemsCount);
            TotalItemsCount = totalItemsCount;
        }

        private int countPages(int size, int itemsCount)
        {
            return (int)Math.Ceiling((double)itemsCount / size);
        }
    }
}