namespace TicketTrader.Orders.Domain.Queries
{
    class GetCustomerTicketsForEvent : IQueryMessage
    {
        public string CustomerId { get; }
        public string OrderId { get; }
        public string EventId { get; }

        public GetCustomerTicketsForEvent(string customerId, string orderId, string eventId)
        {
            CustomerId = customerId;
            OrderId = orderId;
            EventId = eventId;
        }
    }
}