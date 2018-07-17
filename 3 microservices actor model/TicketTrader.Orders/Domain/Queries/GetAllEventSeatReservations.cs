namespace TicketTrader.Orders.Domain.Queries
{
    class GetAllEventSeatReservations : IQueryMessage
    {
        public string EventId { get; }

        public GetAllEventSeatReservations(string eventId)
        {
            EventId = eventId;
        }
    }
}