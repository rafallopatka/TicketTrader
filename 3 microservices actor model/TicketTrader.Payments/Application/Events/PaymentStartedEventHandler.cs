using System.Threading.Tasks;
using TicketTrader.Payments.Domain;
using TicketTrader.Payments.ReadModel;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Payments.Application.Events
{
    public class PaymentStartedEventHandler: IEventHandler<PaymentStarted>
    {
        private readonly IRepository<Payment> _paymentRepository;
        private readonly IPaymentDenormalizer _denormalizer;

        public PaymentStartedEventHandler(IRepository<Payment> paymentRepository, IPaymentDenormalizer denormalizer)
        {
            _paymentRepository = paymentRepository;
            _denormalizer = denormalizer;
        }

        public async Task Handle(PaymentStarted domainEvent)
        {
            var payment = await _paymentRepository.Get(domainEvent.PaymentId);
            await _denormalizer.AddPayment(payment);
        }
    }
}
