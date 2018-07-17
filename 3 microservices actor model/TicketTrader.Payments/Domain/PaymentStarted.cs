using System;
using System.Runtime.Serialization;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Payments.Domain
{
    [DataContract]
    public class PaymentStarted : DomainEvent
    {
        protected PaymentStarted()
        {
            
        }

        public PaymentStarted(Id id, Id orderId, DateTime dateTime)
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