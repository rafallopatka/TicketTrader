using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketTrader.Dal;
using TicketTrader.Model;
using TicketTrader.Services.Core;

namespace TicketTrader.Services.Domain.Orders.OrdersManagement
{
    public class OrderManagementService : IOrderManagementService
    {
        private readonly DalContext _dalContext;
        private readonly ICurrentTimeProvider _timeProvider;

        public OrderManagementService(DalContext dalContext, ICurrentTimeProvider timeProvider)
        {
            _dalContext = dalContext;
            _timeProvider = timeProvider;
        }

        public async Task PayOrdersAsync()
        {
            var orders = await _dalContext
                .Orders
                .Where(x => x.State == OrderState.Commited)
                .ToListAsync();

            orders.ForEach(x => x.State = OrderState.Paid);

            await _dalContext.SaveChangesAsync();
        }

        public async Task DiscardOrdersAsync()
        {
            var now = _timeProvider.Local;

            var ordersToDiscard = await _dalContext
                .Orders
                .Where(x => x.State == OrderState.Active)
                .Where(x => x.CreateDateTime.Add(x.ExpirationTimeout) < now)
                .Include(x => x.Reservations)
                .Select(x => x)
                .ToListAsync();

            ordersToDiscard
                .ForEach(x =>
                {
                    x.State = OrderState.Expired;
                });

            var reservationsToRemove = ordersToDiscard
                .SelectMany(x => x.Reservations)
                .ToList();

            reservationsToRemove
                .ForEach(x =>
                {
                    x.Discarded = true;
                    x.Ticket = null;
                });

            await _dalContext.SaveChangesAsync();
        }

        public async Task DeliverOrderAsync()
        {
            var orders = await _dalContext
                .Orders.Where(x => x.State == OrderState.Paid)
                .ToListAsync();

            orders.ForEach(x => x.State = OrderState.Delivered);

            await _dalContext.SaveChangesAsync();
        }
    }
}