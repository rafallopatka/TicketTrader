using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TicketTrader.Payments.Canonical.Queries;
using TicketTrader.Payments.ReadModel;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Payments.Application.Queries.Handlers
{
    class GetPaymentTypesQueryHandler : IQueryHandler<GetPaymentTypesQuery, GetPaymentTypesQuery.Response>
    {
        private readonly IPaymentsFinder _finder;

        public GetPaymentTypesQueryHandler(IPaymentsFinder finder)
        {
            _finder = finder;
        }

        public async Task<GetPaymentTypesQuery.Response> Handle(GetPaymentTypesQuery query)
        {
            var types = await _finder.GetPaymentTypesAsync();
            var result = types.Select(x => new PaymentTypeDto
            {
                NetAmount = x.NetAmount,
                GrossAmount = x.GrossAmount,
                VatRate = x.VatRate,
                Name = x.Name,
                PaymentTypeId = x.PaymentTypeId
            }).ToArray();

            return new GetPaymentTypesQuery.Response
            {
                Value = new ReadOnlyCollection<PaymentTypeDto>(result).ToList()
            };
        }
    }
}