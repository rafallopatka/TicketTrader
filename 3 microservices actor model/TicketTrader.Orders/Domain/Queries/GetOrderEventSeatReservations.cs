namespace TicketTrader.Orders.Domain.Queries
{
    class GetOrderEventSeatReservations : IQueryMessage
    {
        public string ClientId { get; }
        public string EventId { get; }
        public string OrderId { get; }

        public GetOrderEventSeatReservations(string clientId, string eventId, string orderId)
        {
            ClientId = clientId;
            EventId = eventId;
            OrderId = orderId;
        }
    }
}