namespace TicketTrader.Orders.Domain.Events
{
    class OrderCompleted : IEventMessage
    {
        public string OrderId { get; }
        public string CustomerId { get; }

        public OrderCompleted(string orderId, string customerId)
        {
            OrderId = orderId;
            CustomerId = customerId;
        }
    }
}