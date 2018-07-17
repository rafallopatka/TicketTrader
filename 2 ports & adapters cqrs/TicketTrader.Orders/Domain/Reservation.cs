using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Orders.Domain
{
    public class Reservation : DomainEntity
    {
        public Id EventId { get; protected set; }
        public Seat Seat { get; protected set; }

        public Reservation(Id id, Id eventId, Seat seat)
        {
            EventId = eventId;
            Seat = seat;
            Id = id;
        }
    }
}