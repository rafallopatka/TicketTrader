using System;
using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Commands;
using TicketTrader.Shared.Base.CQRS.Commands;
using TicketTrader.Shared.Base.Infrastructure;

namespace TicketTrader.Api.Services.ClientsOrders
{
    public class ClientsOrderService    
    {
        public async Task<ClientOrderDto> CreateOrderForClientAsync(string clientId)
        {
            var command = new CreateNewOrderCommand
            {
                OrderId = Guid.NewGuid().ToString(),
                ClientId = clientId,
            };

            await CommandBus.Current.DispatchAsync(command);

            var order = new ClientOrderDto
            {
                Id = command.OrderId,
                ClientId = command.ClientId,
            };

            return order;
        }

        public async Task CommitOrderAsync(string clientId, string orderId)
        {
            var command = new CommitOrderCommand
            {
                ClientId = clientId,
                OrderId = orderId
            };

            await CommandBus.Current.DispatchAsync(command);
        }
    }
}