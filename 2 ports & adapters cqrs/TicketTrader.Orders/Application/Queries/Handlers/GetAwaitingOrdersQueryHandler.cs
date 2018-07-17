using System.Linq;
using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Queries;
using TicketTrader.Orders.ReadModel.Orders;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Orders.Application.Queries.Handlers
{
    class GetAwaitingOrdersQueryHandler : IQueryHandler<GetAwaitingOrdersQuery, GetAwaitingOrdersQuery.Response>
    {
        private readonly IOrderFinder _orderFinder;

        public GetAwaitingOrdersQueryHandler(IOrderFinder orderFinder)
        {
            _orderFinder = orderFinder;
        }

        public async Task<GetAwaitingOrdersQuery.Response> Handle(GetAwaitingOrdersQuery query)
        {
            var response = await _orderFinder.GetAwaitingOrders();
            return new GetAwaitingOrdersQuery.Response
            {
                Orders = response.Select(x => new GetAwaitingOrdersQuery.Order
                {
                    Id = x.Id
                }).ToList()
            };
        }
    }
}