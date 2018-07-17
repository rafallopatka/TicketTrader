
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketTrader.Services.Domain.Orders.OrderPayments
{
    public interface IOrderPaymentsService
    {
        Task<IEnumerable<OrderPaymentDto>> GetSelectedPaymentAsync(int clientId, int orderId);
        Task SelectPaymentAsync(int clientId, int orderId, int selectedPaymentOption);
    }
}
