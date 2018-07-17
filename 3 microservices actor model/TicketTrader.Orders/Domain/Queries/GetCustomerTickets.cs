namespace TicketTrader.Orders.Domain.Queries
{
    class GetCustomerTickets : IQueryMessage
    {
        public string CustomerId { get; }
        public string OrderId { get; }

        public GetCustomerTickets(string customerId, string orderId)
        {
            CustomerId = customerId;
            OrderId = orderId;
        }
    }
}