using System.Threading.Tasks;
using TicketTrader.Payments.Canonical.Commands;
using TicketTrader.Shared.Base.CQRS.Commands;
using TicketTrader.Shared.Base.DDD;
using TicketTrader.Shared.Kernel;

namespace TicketTrader.Payments.Application.Services
{
    public class PaymentService : IEventHandler<NewOrderCommitedEvent>
    {
        public async Task Handle(NewOrderCommitedEvent domainEvent)
        {
            await CommandBus.Current.DispatchAsync(new RegisterNewPaymentCommand
            {
                OrderId = domainEvent.OrderId.ToString(),
                PaymentOptionId = domainEvent.PaymentMethodId.ToString()
            });
        }
    }
}
