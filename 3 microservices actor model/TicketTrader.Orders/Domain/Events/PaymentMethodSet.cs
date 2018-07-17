namespace TicketTrader.Orders.Domain.Events
{
    class PaymentMethodSet : IEventMessage
    {
        public string OrderId { get; }
        public string PaymentTypeId { get; }

        public PaymentMethodSet(string orderId, string paymentTypeId)
        {
            PaymentTypeId = paymentTypeId;
            OrderId = orderId;
        }
    }
}