using System.Collections.Generic;
using System.Runtime.Serialization;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Canonical.Queries
{
    [DataContract]
    public class GetAllEventSeatReservationsQuery : IQuery
    {
        [DataMember]
        public string EventId { get; set; }

        [DataContract]
        public class Response: IQueryResponse
        {
            [DataMember]
            public List<SeatReservation> Reservations { get; set; }
        }

        [DataContract]
        public class SeatReservation
        {
            [DataMember]
            public string Id { get; set; }

            [DataMember]
            public string SceneSeatId { get; set; }
        }
    }
}