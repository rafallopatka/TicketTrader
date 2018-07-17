using System.Runtime.Serialization;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Orders.Domain
{
    [DataContract]
    public class OrderCommitedEvent : DomainEvent
    {
        protected OrderCommitedEvent()
        {
            
        }

        public OrderCommitedEvent(Id id)
        {
            OrderId = id;
        }

        [DataMember]
        public Id OrderId { get; protected set; }
    }
}