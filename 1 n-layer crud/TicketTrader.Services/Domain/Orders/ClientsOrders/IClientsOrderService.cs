using System.Threading.Tasks;

namespace TicketTrader.Services.Domain.Orders.ClientsOrders
{
    public interface IClientsOrderService
    {
        Task<ClientOrderDto> CreateOrderForClientAsync(int clientId);
        Task CommitOrderAsync(int clientId, int orderId);
    }
}