using System.Runtime.Serialization;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Orders.Domain
{
    [DataContract]
    public class OrderDiscardedEvent: DomainEvent
    {
        protected OrderDiscardedEvent()
        {
            
        }

        public OrderDiscardedEvent(Id id)
        {
            OrderId = id;
        }

        [DataMember]
        public Id OrderId { get; protected set; }
    }
}