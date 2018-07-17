namespace TicketTrader.Orders.Domain.Events
{
    class ReservationCreated : IEventMessage
    {
        public string CustomerId { get; }
        public string OrderId { get; }
        public string ReservationId { get; }
        public string SceneSeatId { get; }
        public string EventId { get; }

        public ReservationCreated(string customerId, string orderId, string eventId, string reservationId, string sceneSeatId)
        {
            CustomerId = customerId;
            OrderId = orderId;
            ReservationId = reservationId;
            SceneSeatId = sceneSeatId;
            EventId = eventId;
        }
    }
}