namespace TicketTrader.Orders.Domain.Commands
{
    class ReserveSeat : ICommandMessage
    {
        public string CustomerId { get; }
        public string OrderId { get; }
        public string EventId { get; }
        public string ReservationId { get; }
        public string SceneSeatId { get; }

        public ReserveSeat(string customerId, string orderId, string eventId, string reservationId, string sceneSeatId)
        {
            CustomerId = customerId;
            OrderId = orderId;
            EventId = eventId;
            ReservationId = reservationId;
            SceneSeatId = sceneSeatId;
        }
    }
}