namespace TicketTrader.Orders.Domain.Events
{
    class OrderCreated : IEventMessage
    {
        public string CustomerId { get; }
        public string OrderId { get; }

        public OrderCreated(string customerId, string orderId)
        {
            CustomerId = customerId;
            OrderId = orderId;
        }
    }
}