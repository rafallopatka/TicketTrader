using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Canonical.Queries
{
    public class GetSelectedPaymentQuery : IQuery
    {
        public string ClientId { get; set; }
        public string OrderId { get; set; }

        public class Response : IQueryResponse
        {
            public PaymentDto Value { get; set; }
        }

        public class PaymentDto
        {
            public string OrderId { get; set; }
            public string PaymentId { get; set; }
        }
    }
}