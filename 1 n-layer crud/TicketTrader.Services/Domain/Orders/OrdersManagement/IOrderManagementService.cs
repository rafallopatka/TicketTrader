using System.Threading.Tasks;

namespace TicketTrader.Services.Domain.Orders.OrdersManagement
{
    public interface IOrderManagementService
    {
        Task PayOrdersAsync();
        Task DiscardOrdersAsync();
        Task DeliverOrderAsync();
    }
}