using System.Collections.Generic;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Canonical.Queries
{
    public class GetClientTicketsForEventQuery: IQuery
    {
        public string EventId { get; set; }
        public string ClientId { get; set; }
        public string OrderId { get; set; }

        public class Response : IQueryResponse
        {
            public List<ClientTicketsForOrder> Tickets { get; set; }
        }

        public class ClientTicketsForOrder
        {
            public string Id { get; set; }

            public string EventId { get; set; }
            public string ClientId { get; set; }
            public IList<string> SceneSeatIds { get; set; }

            public string PriceOptionId { get; set; }
            public string OrderId { get; set; }

            public string PriceZoneName { get; set; }
            public string PriceOptionName { get; set; }
            public decimal GrossAmount { get; set; }

            public ClientTicketsForOrder()
            {
                SceneSeatIds = new List<string>();
            }
        }
    }
}