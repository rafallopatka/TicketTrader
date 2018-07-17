using System;

namespace TicketTrader.Api.Services.ClientsOrders
{
    public class ClientOrderDto
    {
        public string Id { get; set; }
        public string ClientId { get; set; }

        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public TimeSpan ExpirationTimeout { get; set; }
    }
}