using System;
using System.Runtime.Serialization;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Payments.Canonical.Events
{
    [DataContract]
    public class PaymentFailed : DomainEvent
    {
        protected PaymentFailed()
        {
            
        }

        public PaymentFailed(Id id, Id orderId, DateTime dateTime)
        {
            PaymentId = id;
            OrderId = orderId;
            DateTime = dateTime;
        }

        [DataMember]
        public DateTime DateTime { get; protected set; }
        [DataMember]
        public Id PaymentId { get; protected set; }
        [DataMember]
        public Id OrderId { get; protected set; }
    }
}