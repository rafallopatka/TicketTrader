using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Deliveries.Domain
{
    public class Order : DomainEntity
    {
        public Order(string orderId)
        {
            Id = Id.From(orderId);
        }
    }
}
