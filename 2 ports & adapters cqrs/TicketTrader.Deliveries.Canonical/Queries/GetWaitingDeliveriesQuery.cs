using System.Collections.Generic;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Deliveries.Canonical.Queries
{
    public class GetWaitingDeliveriesQuery: IQuery
    {
        public class Response : IQueryResponse
        {
            public List<Delivery> Value { get; set; }
        }

        public class Delivery
        {
            public string OrderId { get; set; }
            public string DeliveryId { get; set; }
        }
    }
}
