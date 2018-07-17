namespace TicketTrader.Orders.Domain.Commands
{
    class DiscardOrder : ICommandMessage, IOrderMessage
    {
        public string OrderId { get; }

        public DiscardOrder(string orderId)
        {
            OrderId = orderId;
        }
    }
}