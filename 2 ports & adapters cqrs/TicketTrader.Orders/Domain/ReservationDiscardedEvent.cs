using System.Runtime.Serialization;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Orders.Domain
{
    [DataContract]
    public class ReservationDiscardedEvent : DomainEvent
    {
        protected ReservationDiscardedEvent()
        {

        }

        public ReservationDiscardedEvent(Id id, Id reservationId, Id seatId)
        {
            OrderId = id;
            ReservationId = reservationId;
            SeatId = seatId;
        }

        [DataMember]
        public Id OrderId { get; protected set; }
        [DataMember]
        public Id ReservationId { get; protected set; }
        [DataMember]
        public Id SeatId { get; protected set; }
    }
}