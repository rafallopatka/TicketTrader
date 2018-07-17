using System;
using System.Collections.Generic;

namespace TicketTrader.Model
{
    public class Event
    {
        public int Id { get; set; }
        public Event Root { get; set; }
        public DateTime DateTime { get; set; }
        public TimeSpan Duration { get; set; }

        public int DescriptionId { get; set; }
        public EventDescription Description { get; set; }
        public Scene Scene { get; set; }

        public ICollection<PriceList> PriceLists { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Seat> Seats { get; set; }
    }
}