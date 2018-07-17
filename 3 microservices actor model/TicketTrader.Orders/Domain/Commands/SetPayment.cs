namespace TicketTrader.Orders.Domain.Commands
{
    class SetPayment : ICommandMessage, IOrderMessage
    {
        public string CustomerId { get; }
        public string OrderId { get; }
        public string PaymentTypeId { get; }

        public SetPayment(string clientId, string orderId, string paymentTypeId)
        {
            CustomerId = clientId;
            OrderId = orderId;
            PaymentTypeId = paymentTypeId;
        }
    }
}