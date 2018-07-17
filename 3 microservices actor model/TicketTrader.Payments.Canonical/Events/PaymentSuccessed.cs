using System;
using System.Runtime.Serialization;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Payments.Canonical.Events
{
    [DataContract]
    public class PaymentSuccessed : DomainEvent
    {
        protected PaymentSuccessed()
        {
            
        }

        public PaymentSuccessed(Id id, Id orderId, DateTime dateTime)
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