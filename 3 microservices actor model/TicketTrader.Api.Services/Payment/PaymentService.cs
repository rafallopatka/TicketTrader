using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketTrader.Payments.Canonical.Commands;
using TicketTrader.Payments.Canonical.Queries;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Api.Services.Payment
{
    public class PaymentService
    {
        public async Task<IEnumerable<PaymentDto>> GetWaitingPaymentsAsync()
        {
            var query = new GetWaitingPaymentsQuery();
            var data = await QueryBus.Current.Query<GetWaitingPaymentsQuery, GetWaitingPaymentsQuery.Response>(query);

            return data.Value.Select(x => new PaymentDto
            {
                OrderId = x.OrderId
            });
        }

        public async Task RegisterNewPaymentAsync(PaymentDto payment)
        {
            var command = new RegisterNewPaymentCommand
            {
                OrderId = payment.OrderId
            };

            await CommandBus.Current.DispatchAsync(command);
        }

        public async Task RegisterPaymentSuccessAsync(PaymentDto payment)
        {
            var query = new GetPaymentForOrderQuery { OrderId = payment.OrderId };
            var result = await QueryBus.Current.Query<GetPaymentForOrderQuery, GetPaymentForOrderQuery.Response>(query);

            var command = new RegisterPaymentSuccessCommand
            {
                PaymentId = result.Value.PaymentId
            };

            await CommandBus.Current.DispatchAsync(command);
        }

        public async Task RegisterPaymenFailureAsync(PaymentDto payment)
        {
            var query = new GetPaymentForOrderQuery {OrderId = payment.OrderId};
            var result = await QueryBus.Current.Query<GetPaymentForOrderQuery, GetPaymentForOrderQuery.Response>(query);

            var command = new RegisterPaimentFailureCommand
            {
                PaymentId = result.Value.PaymentId
            };

            await CommandBus.Current.DispatchAsync(command);
        }
    }
}