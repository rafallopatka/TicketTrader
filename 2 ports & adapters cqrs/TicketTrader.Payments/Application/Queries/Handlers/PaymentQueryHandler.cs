using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using TicketTrader.Payments.Canonical.Queries;
using TicketTrader.Payments.ReadModel;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Payments.Application.Queries.Handlers
{
    class GetPaymentForOrderQueryHandler: IQueryHandler<GetPaymentForOrderQuery, GetPaymentForOrderQuery.Response>
    {
        private readonly IPaymentsFinder _finder;

        public GetPaymentForOrderQueryHandler(IPaymentsFinder finder)
        {
            _finder = finder;
        }

        public async Task<GetPaymentForOrderQuery.Response> Handle(GetPaymentForOrderQuery query)
        {
            var payment = await _finder.GetPaymentForOrderAsync(query.OrderId);

            var value = payment == null
                ? null : new PaymentDto
                {
                    OrderId = payment.OrderId,
                    PaymentId = payment.Id
                };

            return new GetPaymentForOrderQuery.Response
            {
                Value = value
            };
        }
    }
}
