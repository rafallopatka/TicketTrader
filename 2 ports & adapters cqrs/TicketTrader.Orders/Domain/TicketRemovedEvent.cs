using System.Runtime.Serialization;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Orders.Domain
{
    [DataContract]
    public class TicketRemovedEvent : DomainEvent
    {
        protected TicketRemovedEvent()
        {

        }

        public TicketRemovedEvent(Id orderId, Id ticketId)
        {
            OrderId = orderId;
            TicketId = ticketId;
        }

        [DataMember]
        public Id OrderId  { get; protected set; }
        [DataMember]
        public Id TicketId  { get; protected set; }
    }
}