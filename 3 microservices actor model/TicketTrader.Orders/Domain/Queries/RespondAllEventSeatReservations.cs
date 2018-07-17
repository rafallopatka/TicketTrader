using System.Collections.Generic;
using TicketTrader.Orders.Domain.Queries.ReadModels;

namespace TicketTrader.Orders.Domain.Queries
{
    public class RespondAllEventSeatReservations
    {
        public IReadOnlyCollection<SeatReservationReadModel> Reservations { get;}

        public RespondAllEventSeatReservations(IEnumerable<SeatReservationReadModel> collection)
        {
            Reservations = new List<SeatReservationReadModel>(collection);
        }
    }
}