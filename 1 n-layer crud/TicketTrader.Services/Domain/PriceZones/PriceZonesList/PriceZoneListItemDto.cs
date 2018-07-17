using System.Collections.Generic;

namespace TicketTrader.Services.Domain.PriceZones.PriceZonesList
{
    public class PriceZoneListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ScenePriceZoneId { get; set; }
        public IEnumerable<PriceOption> Options { get; set; }

        public class PriceOption
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int PriceId { get; set; }
            public decimal GrossAmount { get; set; }
            public decimal NetAmount { get; set; }
            public decimal VatRate { get; set; }
        }
    }
}