using System.Collections.Generic;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Payments.Canonical.Queries
{
    public class GetWaitingPaymentsQuery: IQuery
    {
        public class Response: IQueryResponse
        {
            public List<PaymentDto> Value { get; set; }
        }
    }
}
