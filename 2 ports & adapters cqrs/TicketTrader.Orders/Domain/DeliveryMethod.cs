using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Orders.Domain
{
    public class DeliveryMethod: DomainEntity
    {
        public Id Option { get; protected set; }

        public DeliveryMethod(Id id, Id option)
        {
            Id = id;
            Option = option;
        }
    }
}