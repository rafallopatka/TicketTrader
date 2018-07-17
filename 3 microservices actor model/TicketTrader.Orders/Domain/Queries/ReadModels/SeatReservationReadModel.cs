namespace TicketTrader.Orders.Domain.Queries.ReadModels
{
    public class SeatReservationReadModel
    {
        public SeatReservationReadModel(string id, string sceneSeatId, string eventId, string orderId)
        {
            Id = id;
            SceneSeatId = sceneSeatId;
            EventId = eventId;
            OrderId = orderId;
        }

        public string Id { get; }
        public string SceneSeatId { get; }
        public string EventId { get; }
        public string OrderId { get; }
    }
}