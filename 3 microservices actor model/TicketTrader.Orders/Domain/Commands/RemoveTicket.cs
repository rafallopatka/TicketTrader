namespace TicketTrader.Orders.Domain.Commands
{
    class RemoveTicket : ICommandMessage, IOrderMessage
    {
        public string CustomerId { get; }
        public string OrderId { get; }
        public string EventId { get; }
        public string TicketId { get; }

        public RemoveTicket(string customerId, string orderId, string eventId, string ticketId)
        {
            CustomerId = customerId;
            OrderId = orderId;
            EventId = eventId;
            TicketId = ticketId;
        }
    }
}