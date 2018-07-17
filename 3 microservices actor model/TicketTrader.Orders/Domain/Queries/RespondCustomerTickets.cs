using System.Collections.Generic;
using TicketTrader.Orders.Domain.Queries.ReadModels;

namespace TicketTrader.Orders.Domain.Queries
{
    class RespondCustomerTickets
    {
        public IReadOnlyCollection<OrderTicketReadModel> Tickets { get; }

        public RespondCustomerTickets(IEnumerable<OrderTicketReadModel> tickets)
        {
            Tickets = new List<OrderTicketReadModel>(tickets);
        }
    }
}