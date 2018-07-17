using System.Collections.Generic;
using TicketTrader.Orders.Domain.Queries.ReadModels;

namespace TicketTrader.Orders.Domain.Queries
{
    class RespondCustomerTicketsForEvent
    {
        public IReadOnlyCollection<OrderTicketReadModel> Tickets { get; }

        public RespondCustomerTicketsForEvent(IEnumerable<OrderTicketReadModel> tickets)
        {
            Tickets = new List<OrderTicketReadModel>(tickets);
        }
    }
}