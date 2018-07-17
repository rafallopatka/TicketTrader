namespace TicketTrader.Orders.Domain.Commands
{
    class DiscardSeat : ICommandMessage
    {
        public string OrderId { get; }
        public string CustomerId { get; }
        public string EventId { get; }
        public string ReservationId { get; }
        public string SceneSeatId { get; }

        public DiscardSeat(string orderId, string customerId, string eventId, string reservationId, string sceneSeatId)
        {
            OrderId = orderId;
            CustomerId = customerId;
            EventId = eventId;
            ReservationId = reservationId;
            SceneSeatId = sceneSeatId;
        }
    }
}