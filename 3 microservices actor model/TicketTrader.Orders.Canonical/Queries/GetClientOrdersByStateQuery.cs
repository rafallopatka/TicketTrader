using System;
using System.Collections.Generic;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Canonical.Queries
{
    public class GetClientOrdersByStateQuery: IQuery
    {
        public string ClientId { get; set; }
        public OrderState? State { get; set; }

        public enum OrderState
        {
            Active,
            Expired,
            Commited,
            Finalized,
            Canceled
        }

        public class Response: IQueryResponse
        {
            public List<ClientOrder> ClientOrders { get; set; } 
        }

        public class ClientOrder
        {
            public string Id { get; set; }
            public string ClientId { get; set; }

            public DateTime CreateDateTime { get; set; }
            public DateTime UpdateDateTime { get; set; }
            public TimeSpan ExpirationTimeout { get; set; }
        }
    }
}