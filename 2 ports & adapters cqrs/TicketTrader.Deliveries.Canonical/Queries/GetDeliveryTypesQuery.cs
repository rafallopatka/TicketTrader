using System.Collections.Generic;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Deliveries.Canonical.Queries
{
    public class GetDeliveryTypesQuery: IQuery
    {
        public class Response : IQueryResponse
        {
            public List<DeliveryType> Value { get; set; }
        }

        public class DeliveryType
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public decimal GrossAmount { get; set; }
            public decimal NetAmount { get; set; }
            public decimal VatRate { get; set; }
        }
    }
}
