using System.Collections.Generic;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Canonical.Queries
{
    public class GetAwaitingOrdersQuery : IQuery
    {
        public class Response : IQueryResponse
        {
            public List<Order> Orders { get; set; }
        }

        public class Order
        {
            public string Id { get; set; }
        }
    }
}