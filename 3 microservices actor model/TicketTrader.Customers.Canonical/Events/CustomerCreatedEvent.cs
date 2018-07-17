using System;
using System.Runtime.Serialization;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Customers.Canonical.Events
{
    [DataContract]
    public class CustomerCreatedEvent: DomainEvent
    {
        [DataMember]
        public Id CustomerId { get; protected set; }

        protected CustomerCreatedEvent()
        {
            
        }

        public CustomerCreatedEvent(Id customerId)
        {
            CustomerId = customerId;
        }
    }
}
