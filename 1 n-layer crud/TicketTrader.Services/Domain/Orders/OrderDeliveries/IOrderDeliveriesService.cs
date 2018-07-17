using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketTrader.Services.Domain.Orders.OrderDeliveries
{
    public interface IOrderDeliveriesService
    {
        Task<IEnumerable<OrderDeliveryDto>> GetSelectedDeliveryAsync(int clientId, int orderId);
        Task SelectDeliveryAsync(int clientId, int orderId, int selectedDeliveryOption);
    }
}