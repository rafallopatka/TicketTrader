using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketTrader.Services.Domain.Orders.ClientsOrders
{
    public interface IClientsOrdersProvider
    {
        Task<IEnumerable<ClientOrderDto>> GetClientOrdersByStateAsync(int clientId, ClientOrderState? state);
    }
}
