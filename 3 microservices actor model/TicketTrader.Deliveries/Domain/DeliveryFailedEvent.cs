using System.Runtime.Serialization;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Deliveries.Domain
{
    [DataContract]
    public class DeliveryFailedEvent : DomainEvent
    {
        protected DeliveryFailedEvent()
        {
            
        }

        public DeliveryFailedEvent(Id deliveryId)
        {
            DeliveryId = deliveryId;
        }

        [DataMember]
        public Id DeliveryId { get; protected set; }
    }
}