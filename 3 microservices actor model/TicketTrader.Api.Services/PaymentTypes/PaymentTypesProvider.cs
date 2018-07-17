using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketTrader.Payments.Canonical.Queries;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Api.Services.PaymentTypes
{
    public class PaymentTypesProvider
    {
        public async Task<IList<PaymentTypeDto>> GetPaymentTypesAsync()
        {
            var query = new GetPaymentTypesQuery();

            var result = await QueryBus.Current.Query<GetPaymentTypesQuery, GetPaymentTypesQuery.Response>(query);

            return result.Value.Select(x => new PaymentTypeDto
            {
                Id = x.PaymentTypeId,
                Name = x.Name,
                GrossAmount = x.GrossAmount,
                NetAmount = x.NetAmount,
                VatRate = x.VatRate,
            }).ToList();
        }
    }
}