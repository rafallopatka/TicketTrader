using System.Threading.Tasks;
using TicketTrader.Payments.Canonical.Commands;
using TicketTrader.Payments.Domain;
using TicketTrader.Shared.Base.CQRS.Commands;
using TicketTrader.Shared.Base.DDD;
using TicketTrader.Shared.Base.Infrastructure;

namespace TicketTrader.Payments.Application.Commands.Handlers
{
    public class NewPaymentCommandHandler: ICommandHandler<RegisterNewPaymentCommand>
    {
        private readonly PaymentFactory _factory;
        private readonly IRepository<Payment> _paymentRepository;

        public NewPaymentCommandHandler(PaymentFactory factory,  
            IRepository<Payment> paymentRepository)
        {
            _factory = factory;
            _paymentRepository = paymentRepository;
        }

        public async Task HandleAsync(RegisterNewPaymentCommand command)
        {
            var payment = _factory.CreatePayment(command.OrderId, command.PaymentOptionId);
            await _paymentRepository.Save(payment);
            var paymentEvent = new PaymentStarted(payment.Id, payment.Order.Id, CurrentDateTime.Local);
            await EventBus.Current.PublishAsync(paymentEvent);
        }
    }
}
