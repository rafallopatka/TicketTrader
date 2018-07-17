using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketTrader.Dal;
using TicketTrader.Services.Mappings;

namespace TicketTrader.Services.Domain.Orders.OrderPayments
{
    public class OrderPaymentsService : IOrderPaymentsService
    {
        private readonly DalContext _dalContext;

        public OrderPaymentsService(DalContext dalContext)
        {
            _dalContext = dalContext;
        }

        public async Task<IEnumerable<OrderPaymentDto>> GetSelectedPaymentAsync(int clientId, int orderId)
        {
            var data = await _dalContext
                .Orders
                .Where(x => x.Id == orderId)
                .Where(x => x.ClientId == clientId)
                .Where(x => x.PaymentId.HasValue)
                .AsNoTracking()
                .ToListAsync();

            return data.MapTo<IEnumerable<OrderPaymentDto>>();
        }

        public async Task SelectPaymentAsync(int clientId, int orderId, int selectedPaymentOption)
        {
            var payment = await _dalContext
                .Payments
                .SingleAsync(x => x.Id == selectedPaymentOption);

            var order = await _dalContext
                .Orders
                .SingleAsync(x => x.ClientId == clientId && x.Id == orderId);

            order.Payment = payment;

            await _dalContext.SaveChangesAsync();
        }
    }
}