using System;
using System.Collections.Generic;

namespace TicketTrader.Model
{
    public class Scene
    {
        public int Id { get; set; }
        public Scene Root { get; set; }
        public IEnumerable<Seat> Seats { get; set; }
        public string DisplayName { get; set; }
        public string UniqueName { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}