using System.Collections.Generic;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Canonical.Queries
{
    public class GetOrderEventSeatReservationsQuery : IQuery
    {
        public string EventId { get; set; }
        public string ClientId { get; set; }
        public string OrderId { get; set; }

        public class Response: IQueryResponse
        {
            public List<SeatReservation> SeatReservations { get; set; }
        }

        public class SeatReservation
        {
            public string Id { get; set; }
            public string SceneSeatId { get; set; }
        }
    }
}