using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Commands;
using TicketTrader.Orders.Canonical.Queries;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Api.Services.Orders
{
    public class OrderService
    {
        public async Task<IEnumerable<OrderDto>> GetAwaitingOrdersAsync()
        {
            var query = new GetAwaitingOrdersQuery();

            var data = await QueryBus.Current.Query<GetAwaitingOrdersQuery, GetAwaitingOrdersQuery.Response>(query);

            return data.Orders.Select(x => new OrderDto
            {
                OrderId = x.Id
            });
        }

        public async Task DiscardOrderAsync(string orderId)
        {
            var command = new DiscardOrderCommand
            {
                OrderId = orderId
            };

            await CommandBus.Current.DispatchAsync(command);
        }

        public async Task CompleteOrderAsync(string orderId)
        {
            var command = new CompleteOrderCommand
            {
                OrderId = orderId
            };

            await CommandBus.Current.DispatchAsync(command);
        }
    }
}