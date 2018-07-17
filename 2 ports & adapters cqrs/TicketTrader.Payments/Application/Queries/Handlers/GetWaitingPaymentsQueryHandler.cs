using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TicketTrader.Payments.Canonical.Queries;
using TicketTrader.Payments.ReadModel;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Payments.Application.Queries.Handlers
{
    class GetWaitingPaymentsQueryHandler : IQueryHandler<GetWaitingPaymentsQuery, GetWaitingPaymentsQuery.Response>
    {
        private readonly IPaymentsFinder _finder;

        public GetWaitingPaymentsQueryHandler(IPaymentsFinder finder)
        {
            _finder = finder;
        }

        public async Task<GetWaitingPaymentsQuery.Response> Handle(GetWaitingPaymentsQuery query)
        {
            var payments = await _finder.GetWaitingPaymentsAsync();
            var result = payments.Select(x => new PaymentDto
            {
                OrderId = x.OrderId,
                PaymentId = x.Id
            }).ToArray();

            return new GetWaitingPaymentsQuery.Response
            {
                Value = new ReadOnlyCollection<PaymentDto>(result).ToList()
            };
        }
    }
}