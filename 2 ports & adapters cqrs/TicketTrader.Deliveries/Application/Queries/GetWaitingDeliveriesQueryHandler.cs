using System.Linq;
using System.Threading.Tasks;
using TicketTrader.Deliveries.Canonical.Queries;
using TicketTrader.Deliveries.ReadModel;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Deliveries.Application.Queries
{
    class GetWaitingDeliveriesQueryHandler : IQueryHandler<GetWaitingDeliveriesQuery, GetWaitingDeliveriesQuery.Response>
    {
        private readonly IDeliveryFinder _finder;

        public GetWaitingDeliveriesQueryHandler(IDeliveryFinder finder)
        {
            _finder = finder;
        }

        public async Task<GetWaitingDeliveriesQuery.Response> Handle(GetWaitingDeliveriesQuery query)
        {
            var list = await _finder.GetWaitingDeliveriesAsync();

            return new GetWaitingDeliveriesQuery.Response
            {
                Value = list.Select(x => new GetWaitingDeliveriesQuery.Delivery
                {
                    OrderId = x.OrderId,
                    DeliveryId = x.Id
                }).ToList()
            };
        }
    }
}