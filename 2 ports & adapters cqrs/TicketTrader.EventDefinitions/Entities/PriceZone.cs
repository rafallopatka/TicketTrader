using System.Collections.Generic;

namespace TicketTrader.EventDefinitions.Entities
{
    public class PriceZone
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PriceListId { get; set; }
        public int ScenePriceZoneId { get; set; }

        public ICollection<Seat> Seats { get; set; }
        public ICollection<PriceOption> Options { get; set; }
        public PriceList PriceList { get; set; }
    }
}