namespace TicketTrader.Orders.Domain.Events
{
    class DeliveryMethodSet: IEventMessage
    {
        public string OrderId { get; }
        public string DeliveryMethodId { get; }

        public DeliveryMethodSet(string orderId, string deliveryMethodId)
        {
            OrderId = orderId;
            DeliveryMethodId = deliveryMethodId;
        }
    }
}