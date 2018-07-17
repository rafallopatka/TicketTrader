using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Orders.Domain
{
    public class PaymentMethod : DomainEntity
    {
        public Id Option { get; protected set; }

        public PaymentMethod(Id id, Id option)
        {
            Id = id;
            Option = option;
        }
    }
}