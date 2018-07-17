using System.Runtime.Serialization;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Orders.Domain
{
    [DataContract]
    public class OrderCompletedEvent : DomainEvent
    {
        protected OrderCompletedEvent()
        {
            
        }

        public OrderCompletedEvent(Id id)
        {
            OrderId = id;
        }

        [DataMember]
        public Id OrderId { get; protected set; }
    }
}