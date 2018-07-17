namespace TicketTrader.Orders.Domain.Events
{
    class ReservationDiscarded : IEventMessage
    {
        public string OrderId { get; }
        public string EventId { get; }
        public string ReservationId { get; }

        public ReservationDiscarded(string orderId, string eventId, string reservationId)
        {
            OrderId = orderId;
            EventId = eventId;
            ReservationId = reservationId;
        }
    }
}