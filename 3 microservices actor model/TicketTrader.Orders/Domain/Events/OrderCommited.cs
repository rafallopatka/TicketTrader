namespace TicketTrader.Orders.Domain.Events
{
    class OrderCommited : IEventMessage
    {
        public string OrderId { get; }
        public string CustomerId { get; }

        public OrderCommited(string orderId, string customerId)
        {
            OrderId = orderId;
            CustomerId = customerId;
        }
    }
}