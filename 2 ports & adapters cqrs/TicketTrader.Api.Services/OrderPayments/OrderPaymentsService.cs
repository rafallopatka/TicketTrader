using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Commands;
using TicketTrader.Orders.Canonical.Queries;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Api.Services.OrderPayments
{
    public class OrderPaymentsService
    {
        public async Task<IEnumerable<OrderPaymentDto>> GetSelectedPaymentAsync(string clientId, string orderId)
        {
            var query = new GetSelectedPaymentQuery
            {
                ClientId = clientId,
                OrderId = orderId
            };

            var response = await QueryBus.Current.Query<GetSelectedPaymentQuery, GetSelectedPaymentQuery.Response>(query);

            if (response.Value == null)
            {
                return new OrderPaymentDto[0];
            }

            var value = response.Value;
            return new[]
            {
                new OrderPaymentDto()
                {
                    OrderId = value.OrderId,
                    PaymentId = value.PaymentId
                }
            };
        }

        public async Task SelectPaymentAsync(string clientId, string orderId, string selectedPaymentOption)
        {
            var command = new SelectPaymentCommand
            {
                ClientId = clientId,
                OrderId = orderId,
                PaymentTypeId = selectedPaymentOption
            };

            await CommandBus.Current.DispatchAsync(command);
        }
    }
}