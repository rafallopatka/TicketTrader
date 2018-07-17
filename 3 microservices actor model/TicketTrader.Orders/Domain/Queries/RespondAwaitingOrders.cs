using System.Collections.Generic;
using TicketTrader.Orders.Domain.Queries.ReadModels;

namespace TicketTrader.Orders.Domain.Queries
{
    public class RespondAwaitingOrders
    {
        public IReadOnlyCollection<OrderReadModel> Orders { get; }

        public RespondAwaitingOrders(IEnumerable<OrderReadModel> orders)
        {
            Orders = new List<OrderReadModel>(orders);
        }
    }
}