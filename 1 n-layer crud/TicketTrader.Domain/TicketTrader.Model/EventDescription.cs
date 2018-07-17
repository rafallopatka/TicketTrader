using System.Collections.Generic;

namespace TicketTrader.Model
{
    public class EventDescription
    {
        public string Description { get; set; }
        public string Cast { get; set; }
        public string Authors { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<EventDescriptionCategories> EventCategories { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}