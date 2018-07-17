namespace TicketTrader.Orders.Domain.Commands
{
    class CreateNewOrder : ICommandMessage, IOrderMessage
    {
        public string CustomerId { get; }
        public string OrderId { get; }

        public CreateNewOrder(string orderId, string customerId)
        {
            OrderId = orderId;
            CustomerId = customerId;
        }
    }
}