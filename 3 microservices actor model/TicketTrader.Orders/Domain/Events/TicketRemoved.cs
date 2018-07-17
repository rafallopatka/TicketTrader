namespace TicketTrader.Orders.Domain.Events
{
    class TicketRemoved : IEventMessage
    {
        public string OrderId { get; }
        public string CustomerId { get; }
        public string TicketId { get; }
        public string EventId { get; }

        public TicketRemoved(string orderId,
            string customerId, 
            string eventId,
            string ticketId)
        {
            OrderId = orderId;
            EventId = eventId;
            CustomerId = customerId;
            TicketId = ticketId;
        }
    }
}