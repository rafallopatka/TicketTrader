using System.Collections.Generic;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Payments.Canonical.Queries
{
    public class GetPaymentTypesQuery : IQuery
    {
        public class Response: IQueryResponse
        {
            public List<PaymentTypeDto> Value { get; set; }
        }
    }
}