namespace TicketTrader.EventDefinitions.Entities
{
    public class EventDescriptionCategories
    {
        public EventCategory Category { get; set; }
        public EventDescription Description { get; set; }
        public int DescriptionId { get; set; }
        public int CategoryId { get; set; }
    }
}