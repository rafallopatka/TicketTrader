using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketTrader.Dal;
using TicketTrader.Model;
using TicketTrader.Services.Core;
using TicketTrader.Services.Mappings;

namespace TicketTrader.Services.Domain.Orders.ClientsOrders
{
    public class ClientsOrderService: IClientsOrderService
    {
        private readonly DalContext _context;
        private readonly ICurrentTimeProvider _currentTime;

        public ClientsOrderService(DalContext context, ICurrentTimeProvider currentTime)
        {
            _context = context;
            _currentTime = currentTime;
        }

        public async Task<ClientOrderDto> CreateOrderForClientAsync(int clientId)
        {
            var now = _currentTime.Local;

            var saveResult = await _context.Orders.AddAsync(new Order
            {
                ClientId = clientId,
                CreateDateTime = now,
                UpdateDateTime = now,
                ExpirationTimeout = TimeSpan.FromMinutes(15)
            });

            await _context.SaveChangesAsync();
            var result = saveResult.Entity.MapTo<ClientOrderDto>();

            return result;
        }

        public async Task CommitOrderAsync(int clientId, int orderId)
        {
            var order = await _context.Orders
                .SingleAsync(x => x.Id == orderId && x.ClientId == clientId && x.State == OrderState.Active);

            order.State = OrderState.Commited;

            await _context.SaveChangesAsync();
        }
    }
}