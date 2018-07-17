using System.Threading.Tasks;
using TicketTrader.Payments.Canonical.Commands;
using TicketTrader.Payments.Domain;
using TicketTrader.Shared.Base.CQRS.Commands;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Payments.Application.Commands.Handlers
{
    public class FailedPaymentCommandHandler : ICommandHandler<RegisterPaimentFailureCommand>
    {
        private readonly IRepository<Payment> _paymentRepository;

        public FailedPaymentCommandHandler(IRepository<Payment> paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task HandleAsync(RegisterPaimentFailureCommand command)
        {
            var paymentEntity = await _paymentRepository.Get(Id.From(command.PaymentId));
            paymentEntity.MakePaymentAsFailed();
            await _paymentRepository.Save(paymentEntity);
        }
    }
}
