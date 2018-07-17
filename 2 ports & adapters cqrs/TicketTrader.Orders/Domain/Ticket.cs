using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Orders.Domain
{
    public class Ticket: DomainEntity
    {
        public Id EventId { get; set; }
        public PriceOption PriceOption { get; set; }
        public Seat Seat { get; set; }

        public Ticket(Id ticketId, Id eventId, Seat seat, PriceOption priceOption)
        {
            Id = ticketId;
            PriceOption = priceOption;
            Seat = seat;
            EventId = eventId;
        }
    }
}