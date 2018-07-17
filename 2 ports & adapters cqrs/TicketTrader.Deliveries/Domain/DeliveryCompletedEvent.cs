using System.Runtime.Serialization;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Deliveries.Domain
{
    [DataContract]
    public class DeliveryCompletedEvent : DomainEvent
    {
        protected DeliveryCompletedEvent()
        {
            
        }

        public DeliveryCompletedEvent(Id deliveryId)
        {
            DeliveryId = deliveryId;
        }

        [DataMember]
        public Id DeliveryId { get; protected set; }
    }
}