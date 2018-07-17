using System.Runtime.Serialization;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Orders.Domain
{
    [DataContract]
    public class DeliveryMethodSetEvent : DomainEvent
    {
        protected DeliveryMethodSetEvent()
        {
            
        }

        public DeliveryMethodSetEvent(Id id, Id deliveryMethodId)
        {
            OrderId = id;
            DeliveryMethodId = deliveryMethodId;
        }

        [DataMember]
        public Id OrderId { get; protected set; }
        [DataMember]
        public Id DeliveryMethodId { get; protected set; }
    }
}