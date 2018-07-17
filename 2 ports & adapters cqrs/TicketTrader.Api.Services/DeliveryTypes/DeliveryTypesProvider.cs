using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketTrader.Deliveries.Canonical.Queries;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Api.Services.DeliveryTypes
{
    public class DeliveryTypesProvider
    {
        public async Task<IList<DeliveryTypeDto>> GetDeliveryTypesAsync()
        {
            var response = await QueryBus.Current.Query<GetDeliveryTypesQuery, GetDeliveryTypesQuery.Response>(new GetDeliveryTypesQuery());

            return response.Value.Select(x => new DeliveryTypeDto
            {
                Name = x.Name,
                Id = x.Id,
                GrossAmount = x.GrossAmount,
                NetAmount = x.NetAmount,
                VatRate = x.VatRate
            }).ToList();
        }
    }
}