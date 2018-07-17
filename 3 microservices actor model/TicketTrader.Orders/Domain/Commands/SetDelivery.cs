namespace TicketTrader.Orders.Domain.Commands
{
    class SetDelivery : ICommandMessage, IOrderMessage
    {
        public SetDelivery(string customerId, string orderId, string deliveryTypeId)
        {
            CustomerId = customerId;
            OrderId = orderId;
            DeliveryTypeId = deliveryTypeId;
        }

        public string CustomerId { get; }
        public string OrderId { get; }
        public string DeliveryTypeId { get; }
    }
}