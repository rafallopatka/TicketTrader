using System.Collections.Generic;
using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Commands;
using TicketTrader.Orders.Canonical.Queries;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Api.Services.OrderDeliveries
{
    public class OrderDeliveriesService
    {
        public async Task<IEnumerable<OrderDeliveryDto>> GetSelectedDeliveryAsync(string clientId, string orderId)
        {
            var query = new GetSelectedDeliveryQuery
            {
                ClientId = clientId,
                OrderId = orderId
            };

            var response = await QueryBus.Current.Query<GetSelectedDeliveryQuery, GetSelectedDeliveryQuery.Response>(query);

            if (response.Value == null)
            {
                return new OrderDeliveryDto[0];
            }


            var value = response.Value;
            return new[]
            {
                new OrderDeliveryDto
                {
                    OrderId = value.OrderId,
                    DeliveryId = value.DeliveryId
                }
            };
        }

        public async Task SelectDeliveryAsync(string clientId, string orderId, string selectedDeliveryOption)
        {
            var command = new SelectDeliveryCommand
            {
                ClientId = clientId,
                OrderId = orderId,
                DeliveryTypeId = selectedDeliveryOption
            };

            await CommandBus.Current.DispatchAsync(command);
        }
    }
}