namespace TicketTrader.Api.Core
{
    public class PagedRequest : ApiRequest
    {
        public int Skip { get; set; }
        public int PageSize { get; set; }
    }
}