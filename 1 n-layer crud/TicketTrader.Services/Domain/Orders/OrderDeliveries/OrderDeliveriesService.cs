using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketTrader.Dal;
using TicketTrader.Model;
using TicketTrader.Services.Core;
using TicketTrader.Services.Mappings;

namespace TicketTrader.Services.Domain.Orders.OrderDeliveries
{
    public class OrderDeliveriesService : IOrderDeliveriesService
    {
        private readonly DalContext _dalContext;
        private readonly ICurrentTimeProvider _timeProvider;

        public OrderDeliveriesService(DalContext dalContext, ICurrentTimeProvider timeProvider)
        {
            _dalContext = dalContext;
            _timeProvider = timeProvider;
        }

        public async Task<IEnumerable<OrderDeliveryDto>> GetSelectedDeliveryAsync(int clientId, int orderId)
        {
            var data = await _dalContext
                .Orders
                .Where<Order>(x => x.Id == orderId)
                .Where(x => x.ClientId == clientId)
                .Where(x => x.DeliveryId.HasValue)
                .AsNoTracking()
                .ToListAsync();

            return data.MapTo<IEnumerable<OrderDeliveryDto>>();
        }

        public async Task SelectDeliveryAsync(int clientId, int orderId, int selectedDeliveryOption)
        {
            var delivery = await _dalContext
                .Deliveries
                .SingleAsync<Delivery>(x => x.Id == selectedDeliveryOption);

            var order = await _dalContext
                .Orders.SingleAsync(x => x.ClientId == clientId && x.Id == orderId);

            order.Delivery = delivery;

            await _dalContext.SaveChangesAsync();
        }
    }
}