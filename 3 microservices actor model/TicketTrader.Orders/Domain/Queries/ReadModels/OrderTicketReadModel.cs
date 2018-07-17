using System.Collections.Generic;

namespace TicketTrader.Orders.Domain.Queries.ReadModels
{
    public class OrderTicketReadModel
    {
        public string Id { get;}

        public string EventId { get;}
        public string ClientId { get;}
        public IReadOnlyCollection<string> SceneSeatIds { get;}

        public string PriceOptionId { get;}
        public string OrderId { get;}

        public string PriceZoneName { get;}
        public string PriceOptionName { get;}
        public decimal GrossAmount { get;}

        public OrderTicketReadModel(string id,
            string eventId,
            string clientId,
            string priceOptionId,
            string orderId,
            string priceZoneName,
            string priceOptionName,
            decimal grossAmount,
            IEnumerable<string> sceneSeatIds)
        {
            Id = id;
            EventId = eventId;
            ClientId = clientId;
            PriceOptionId = priceOptionId;
            OrderId = orderId;
            PriceZoneName = priceZoneName;
            PriceOptionName = priceOptionName;
            GrossAmount = grossAmount;
            SceneSeatIds = new List<string>(sceneSeatIds);
        }
    }
}