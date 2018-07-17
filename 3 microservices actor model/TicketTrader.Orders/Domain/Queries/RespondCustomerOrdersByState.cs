using System.Collections.Generic;
using TicketTrader.Orders.Domain.Queries.ReadModels;

namespace TicketTrader.Orders.Domain.Queries
{
    public class RespondCustomerOrdersByState
    {
        public RespondCustomerOrdersByState(IEnumerable<ClientOrderReadModel> orders)
        {
            Orders = new List<ClientOrderReadModel>(orders);
        }

        public IReadOnlyCollection<ClientOrderReadModel> Orders { get; }
    }
}