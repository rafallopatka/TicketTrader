namespace TicketTrader.Orders.Domain.Events
{
    class OrderDiscarded : IEventMessage
    {
        public string OrderId { get; }
        public string CustomerId { get; }

        public OrderDiscarded(string orderId, string customerId)
        {
            OrderId = orderId;
            CustomerId = customerId;
        }
    }
}