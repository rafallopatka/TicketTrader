using System.Collections.Generic;

namespace TicketTrader.Api.Services.OrderTickets
{
    public class TicketOrderDto
    {
        public string Id { get; set; }

        public string EventId { get; set; }
        public string ClientId { get; set; }
        public IList<string> SceneSeatIds { get; set; }

        public string PriceOptionId { get; set; }
        public string OrderId { get; set; }
        public string PriceZoneName { get; set; }
        public string PriceOptionName { get; set; }
        public decimal GrossAmount { get; set; }

        public TicketOrderDto()
        {
            SceneSeatIds = new List<string>();
        }
    }
}