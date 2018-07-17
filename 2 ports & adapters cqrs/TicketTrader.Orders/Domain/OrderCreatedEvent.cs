using System;
using System.Runtime.Serialization;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Orders.Domain
{
    [DataContract]
    public class OrderCreatedEvent: DomainEvent
    {
        protected OrderCreatedEvent()
        {
            
        }

        public OrderCreatedEvent(Id orderId, Id clientId, DateTime dateTime)
        {
            OrderId = orderId;
            ClientId = clientId;
            DateTime = dateTime;
        }

        [DataMember]
        public Id OrderId { get; protected set; }
        [DataMember]
        public Id ClientId { get; protected set; }
        [DataMember]
        public DateTime DateTime { get; protected set; }
    }
}