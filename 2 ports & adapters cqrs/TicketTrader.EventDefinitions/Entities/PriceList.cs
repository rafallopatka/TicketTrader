using System;
using System.Collections.Generic;

namespace TicketTrader.EventDefinitions.Entities
{
    public class PriceList
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public ICollection<PriceZone> PriceZones { get; set; }
        public DateTime ValidTo { get; set; }
        public DateTime ValidFrom { get; set; }
    }
}