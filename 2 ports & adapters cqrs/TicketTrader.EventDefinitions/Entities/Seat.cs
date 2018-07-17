
namespace TicketTrader.EventDefinitions.Entities
{
    public class Seat
    {
        public int Id { get; set; }
        public int SceneSeatId { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}