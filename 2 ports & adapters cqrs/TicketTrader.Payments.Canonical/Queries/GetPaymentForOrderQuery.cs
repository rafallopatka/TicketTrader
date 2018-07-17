using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Payments.Canonical.Queries
{
    public class GetPaymentForOrderQuery: IQuery
    {
        public string OrderId { get; set; }

        public class Response: IQueryResponse
        {
            public PaymentDto Value { get; set; }
        }
    }
}
