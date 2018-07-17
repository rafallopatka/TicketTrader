namespace TicketTrader.Orders.Domain.Commands
{
    class CompleteOrder : ICommandMessage, IOrderMessage
    {
        public string OrderId { get; }

        public CompleteOrder(string orderId)
        {
            OrderId = orderId;
        }
    }
}