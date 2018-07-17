using System.Collections.Generic;

namespace TicketTrader.Orders.Domain.Actors
{
    class EventReservationsState
    {
        public string EventId { get; set; }

        public List<Reservation> Reservations { get; }

        public EventReservationsState()
        {
            Reservations = new List<Reservation>();
        }
    }
}