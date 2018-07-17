using System.Threading.Tasks;
using TicketTrader.Payments.Canonical.Events;
using TicketTrader.Payments.Domain;
using TicketTrader.Payments.ReadModel;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Payments.Application.Events
{
    public class PaymentFailedEventHandler : IEventHandler<PaymentFailed>
    {
        private readonly IRepository<Payment> _paymentRepository;
        private readonly IPaymentDenormalizer _denormalizer;

        public PaymentFailedEventHandler(IRepository<Payment> paymentRepository, IPaymentDenormalizer denormalizer)
        {
            _paymentRepository = paymentRepository;
            _denormalizer = denormalizer;
        }

        public async Task Handle(PaymentFailed domainEvent)
        {
            var payment = await _paymentRepository.Get(domainEvent.PaymentId);
            await _denormalizer.UpdatePaymentAsync(payment, PaymentStatus.Failed);
        }
    }
}