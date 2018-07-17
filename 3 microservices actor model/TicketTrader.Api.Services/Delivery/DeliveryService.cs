using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketTrader.Shared.Base.CQRS.Commands;
using TicketTrader.Deliveries.Application.Commands;
using TicketTrader.Deliveries.Canonical.Commands;
using TicketTrader.Deliveries.Canonical.Queries;

namespace TicketTrader.Api.Services.Delivery
{
    public class DeliveryService
    {
        public async Task RegisterNewDeliveryAsync(DeliveryDto delivery)
        {
            var command = new RegisterNewDeliveryCommand
            {
                OrderId = delivery.OrderId
            };

            await CommandBus.Current.DispatchAsync(command);
        }

        public async Task RegisterDeliverySuccessAsync(DeliveryDto delivery)
        {
            var command = new RegisterDeliverySuccessCommand
            {
                DeliveryId = delivery.DeliveryId
            };

            await CommandBus.Current.DispatchAsync(command);
        }

        public async Task RegisterDeliveryFailureAsync(DeliveryDto delivery)
        {
            var command = new RegisterDeliveryFailureCommand
            {
                DeliveryId = delivery.DeliveryId
            };

            await CommandBus.Current.DispatchAsync(command);
        }

        public async Task<IEnumerable<DeliveryDto>> GetWaitingDeliveriesAsync()
        {
            var response = await QueryBus.Current.Query<GetWaitingDeliveriesQuery, GetWaitingDeliveriesQuery.Response>(
                    new GetWaitingDeliveriesQuery());

            return response.Value.Select(x => new DeliveryDto
            {
                OrderId = x.OrderId,
                DeliveryId = x.DeliveryId
            });
        }
    }
}