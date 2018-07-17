using System.Collections.Generic;

namespace TicketTrader.Api.Core
{
    public class PagedResponse<TResult> : ApiResponse<IEnumerable<TResult>>
    {
        public int Total { get; set; }
        public int Skip { get; set; }
        public int PageSize { get; set; }
    }
}