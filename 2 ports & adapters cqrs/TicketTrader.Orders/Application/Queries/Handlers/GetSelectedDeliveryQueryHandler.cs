using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Queries;
using TicketTrader.Orders.ReadModel.Deliveries;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Application.Queries.Handlers
{
    class GetSelectedDeliveryQueryHandler : IQueryHandler<GetSelectedDeliveryQuery, GetSelectedDeliveryQuery.Response>
    {
        private readonly IOrderDeliveriesFinder _finder;

        public GetSelectedDeliveryQueryHandler(IOrderDeliveriesFinder finder)
        {
            _finder = finder;
        }

        public async Task<GetSelectedDeliveryQuery.Response> Handle(GetSelectedDeliveryQuery query)
        {
            var response = await _finder.GetSelectedDeliveryAsync(query.ClientId, query.OrderId);

            var value = response == null
                ? null
                : new GetSelectedDeliveryQuery.DeliveryDto
                {
                    OrderId = response.Id,
                    DeliveryId = response.DeliveryId
                };

            return new GetSelectedDeliveryQuery.Response
            {
                Value = value
            };
        }
    }
}