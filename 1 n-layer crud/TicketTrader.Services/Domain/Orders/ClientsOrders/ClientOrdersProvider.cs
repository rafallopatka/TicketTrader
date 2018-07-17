using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketTrader.Dal;
using TicketTrader.Services.Mappings;

namespace TicketTrader.Services.Domain.Orders.ClientsOrders
{
    public class ClientOrdersProvider : IClientsOrdersProvider
    {
        private readonly DalContext _context;

        public ClientOrdersProvider(DalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClientOrderDto>> GetClientOrdersByStateAsync(int clientId, ClientOrderState? state)
        {
            var ordersQuery = _context
                .Orders
                .Where(x => x.ClientId == clientId);

            if (state.HasValue)
                ordersQuery = ordersQuery.Where(x => x.State == (Model.OrderState)state.Value);

            var queryResult = await ordersQuery.ToListAsync();

            return queryResult.MapTo<List<ClientOrderDto>>();
        }
    }
}