using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Queries;
using TicketTrader.Orders.ReadModel.Payments;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Application.Queries.Handlers
{
    class GetSelectedPaymentQueryHandler : IQueryHandler<GetSelectedPaymentQuery, GetSelectedPaymentQuery.Response>
    {
        private readonly IOrderPaymentsFinder _finder;

        public GetSelectedPaymentQueryHandler(IOrderPaymentsFinder finder)
        {
            _finder = finder;
        }

        public async Task<GetSelectedPaymentQuery.Response> Handle(GetSelectedPaymentQuery query)
        {
            var response = await _finder.GetSelectedPaymentAsync(query.ClientId, query.OrderId);

            var value = response == null ? null : new GetSelectedPaymentQuery.PaymentDto
            {
                OrderId = response.Id,
                PaymentId = response.PaymentId
            };

            return new GetSelectedPaymentQuery.Response
            {
                Value = value
            };
        }
    }
}