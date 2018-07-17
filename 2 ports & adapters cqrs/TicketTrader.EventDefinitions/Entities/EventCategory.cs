using System.Collections.Generic;

namespace TicketTrader.EventDefinitions.Entities
{
    public class EventCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<EventDescriptionCategories> EventCategories { get; set; }
    }
}