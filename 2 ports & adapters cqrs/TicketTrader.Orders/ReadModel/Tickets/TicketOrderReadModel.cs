using System.Collections.Generic;
using TicketTrader.Shared.Base.CQRS.ReadModel;

namespace TicketTrader.Orders.ReadModel.Tickets
{
    public class TicketOrderReadModel: IReadModel
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

        public TicketOrderReadModel()
        {
            SceneSeatIds = new List<string>();
        }
    }
}