using System.Runtime.Serialization;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Orders.Domain
{
    [DataContract]
    public class PaymentMethodSetEvent : DomainEvent
    {
        protected PaymentMethodSetEvent()
        {
            
        }

        public PaymentMethodSetEvent(Id orderId, Id paymentMethodId)
        {
            OrderId = orderId;
            PaymentMethodId = paymentMethodId;
        }

        [DataMember]
        public Id OrderId { get; protected set; }
        [DataMember]
        public Id PaymentMethodId { get; protected set; }
    }
}