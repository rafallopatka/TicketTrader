using System.Collections.Generic;

namespace TicketTrader.Services.Domain.Orders.OrderTickets
{
    public class TicketOrderDto
    {
        public int Id { get; set; }
        public string PriceZoneName { get; set; }
        public int PriceZoneId { get; set; }

        public int EventId { get; set; }
        public int ClientId { get; set; }
        public IList<int> SceneSeatIds { get; set; }

        public int PriceOptionId { get; set; }
        public string PriceOptionName { get; set; }

        public decimal GrossAmount { get; set; }

        public TicketOrderDto()
        {
            SceneSeatIds = new List<int>();
        }
    }
}