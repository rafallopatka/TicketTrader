using System.Runtime.Serialization;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Orders.Domain
{
    [DataContract]
    public class ReservationAddedEvent : DomainEvent
    {
        protected ReservationAddedEvent()
        {
            
        }

        public ReservationAddedEvent(Id id, Id reservationId)
        {
            OrderId = id;
            ReservationId = reservationId;
        }

        [DataMember]
        public Id OrderId { get; protected set; }
        [DataMember]
        public Id ReservationId { get; protected set; }
    }
}