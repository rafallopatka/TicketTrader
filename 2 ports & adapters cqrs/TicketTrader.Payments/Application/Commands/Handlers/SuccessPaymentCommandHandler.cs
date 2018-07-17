using System.Threading.Tasks;
using TicketTrader.Payments.Canonical.Commands;
using TicketTrader.Payments.Domain;
using TicketTrader.Shared.Base.CQRS.Commands;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Payments.Application.Commands.Handlers
{
    public class SuccessPaymentCommandHandler : ICommandHandler<RegisterPaymentSuccessCommand>
    {
        private readonly IRepository<Payment> _paymentRepository;

        public SuccessPaymentCommandHandler(
            IRepository<Payment> paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task HandleAsync(RegisterPaymentSuccessCommand command)
        {
            var payment = await _paymentRepository.Get(Id.From(command.PaymentId));

            payment.MakePaymentAsSucceded();
            await _paymentRepository.Save(payment);
        }
    }
}