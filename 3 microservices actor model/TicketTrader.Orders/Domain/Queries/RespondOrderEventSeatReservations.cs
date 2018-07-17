using System.Collections.Generic;
using TicketTrader.Orders.Domain.Queries.ReadModels;

namespace TicketTrader.Orders.Domain.Queries
{
    public class RespondOrderEventSeatReservations
    {
        public IReadOnlyCollection<SeatReservationReadModel> Reservations { get; }

        public RespondOrderEventSeatReservations(IEnumerable<SeatReservationReadModel> collection)
        {
            Reservations = new List<SeatReservationReadModel>(collection);
        }
    }
}