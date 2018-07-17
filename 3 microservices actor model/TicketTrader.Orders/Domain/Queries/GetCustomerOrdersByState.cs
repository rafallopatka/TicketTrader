using TicketTrader.Orders.Domain.Queries.ReadModels;

namespace TicketTrader.Orders.Domain.Queries
{
    class GetCustomerOrdersByState : IQueryMessage
    {
        public OrderStateReadModel State { get; }
        public string CustomerId { get; }

        public GetCustomerOrdersByState(OrderStateReadModel state, string customerId)
        {
            State = state;
            CustomerId = customerId;
        }
    }
}