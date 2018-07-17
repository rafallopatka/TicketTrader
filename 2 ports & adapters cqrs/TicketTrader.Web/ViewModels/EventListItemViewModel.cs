using System;

namespace TicketTrader.Web.ViewModels
{
    public class EventListItemViewModel
    {
        public int Id { get; set; }
        public string Authors { get; set; }
        public string Cast { get; set; }
        public string[] Categories { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public string Title { get; set; }
    }
}