using System.Runtime.Serialization;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Shared.Kernel
{
    [DataContract]
    public class NewOrderCommitedEvent : DomainEvent
    {
        protected NewOrderCommitedEvent()
        {

        }

        public NewOrderCommitedEvent(Id id, Id deliveryMethod, Id paymentMethod)
        {
            OrderId = id;
            DeliveryMethodId = deliveryMethod;
            PaymentMethodId = paymentMethod;
        }

        [DataMember]
        public Id OrderId { get; protected set; }

        [DataMember]
        public Id DeliveryMethodId { get; protected set; }

        [DataMember]
        public Id PaymentMethodId { get; protected set; }
    }
}