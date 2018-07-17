using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Orders.Domain
{
    public class Client: DomainEntity
    {
        public Client(Id id)
        {
            Id = id;
        }
    }
}