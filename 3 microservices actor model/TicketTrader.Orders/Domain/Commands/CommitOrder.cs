namespace TicketTrader.Orders.Domain.Commands
{
    class CommitOrder : ICommandMessage, IOrderMessage
    {
        public CommitOrder(string clientId, string orderId)
        {
            CustomerId = clientId;
            OrderId = orderId;
        }

        public string CustomerId { get; }
        public string OrderId { get; }
    }
}
