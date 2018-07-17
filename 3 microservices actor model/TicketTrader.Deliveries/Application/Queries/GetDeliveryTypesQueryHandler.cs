using System.Linq;
using System.Threading.Tasks;
using TicketTrader.Deliveries.Canonical.Queries;
using TicketTrader.Deliveries.ReadModel;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Deliveries.Application.Queries
{
    class GetDeliveryTypesQueryHandler: IQueryHandler<GetDeliveryTypesQuery, GetDeliveryTypesQuery.Response>
    {
        private readonly IDeliveryFinder _finder;

        public GetDeliveryTypesQueryHandler(IDeliveryFinder finder)
        {
            _finder = finder;
        }

        public async Task<GetDeliveryTypesQuery.Response> Handle(GetDeliveryTypesQuery query)
        {
            var list = await _finder.GetDeliveryTypesAsync();

            return new GetDeliveryTypesQuery.Response
            {
                Value = list.Select(x => new GetDeliveryTypesQuery.DeliveryType
                {
                    Id = x.DeliveryTypeId,
                    GrossAmount = x.GrossAmount,
                    VatRate = x.VatRate,
                    NetAmount = x.NetAmount,
                    Name = x.Name
                }).ToList()
            };
        }
    }
}
