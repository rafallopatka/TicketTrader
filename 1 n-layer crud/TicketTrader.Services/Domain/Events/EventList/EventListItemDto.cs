using System;

namespace TicketTrader.Services.Domain.Events.EventList
{
    public class EventListItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public TimeSpan Duration { get; set; }
        public string Authors { get; set; }
        public string Cast { get; set; }
        public string[] Categories { get; set; }
    }
}
