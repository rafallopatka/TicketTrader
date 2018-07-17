using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Deliveries.Domain
{
    public class DeliveryType: DomainEntity
    {
        public DeliveryType(string id)
        {
            Id = Id.From(id);
        }
    }
}
