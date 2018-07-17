using System.Threading.Tasks;
using TicketTrader.Payments.Canonical.Events;
using TicketTrader.Payments.Domain;
using TicketTrader.Payments.ReadModel;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Payments.Application.Events
{
    public class PaymentSucceededEventHandler : IEventHandler<PaymentSuccessed>
    {
        private readonly IRepository<Payment> _paymentRepository;
        private readonly IPaymentDenormalizer _denormalizer;

        public PaymentSucceededEventHandler(IRepository<Payment> paymentRepository, IPaymentDenormalizer denormalizer)
        {
            _paymentRepository = paymentRepository;
            _denormalizer = denormalizer;
        }

        public async Task Handle(PaymentSuccessed domainEvent)
        {
            var payment = await _paymentRepository.Get(domainEvent.PaymentId);
            await _denormalizer.UpdatePaymentAsync(payment, PaymentStatus.Successed);
        }
    }
}