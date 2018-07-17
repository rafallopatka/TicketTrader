using System.Runtime.Serialization;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Deliveries.Domain
{
    [DataContract]
    public class NewDeliveryRegisteredEvent: DomainEvent
    {
        protected NewDeliveryRegisteredEvent()
        {
            
        }

        public NewDeliveryRegisteredEvent(Id deliveryId)
        {
            DeliveryId = deliveryId;
        }

        [DataMember]
        public Id DeliveryId { get; protected set; }
    }
}
