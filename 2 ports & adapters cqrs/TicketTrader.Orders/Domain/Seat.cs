using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Orders.Domain
{
    public class Seat: DomainEntity
    {
        public Seat(Id id)
        {
            Id = id;
        }
    }
}