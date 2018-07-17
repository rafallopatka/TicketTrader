using System.Threading.Tasks;
using TicketTrader.Orders.Domain;
using TicketTrader.Orders.ReadModel.Payments;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Orders.Application.Events.Handlers
{
    public class PaymentMethodSetEventHandler : IEventHandler<PaymentMethodSetEvent>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IOrderPaymentDenormalizer _denormalizer;

        public PaymentMethodSetEventHandler(IRepository<Order> orderRepository, IOrderPaymentDenormalizer denormalizer)
        {
            _orderRepository = orderRepository;
            _denormalizer = denormalizer;
        }

        public async Task Handle(PaymentMethodSetEvent domainEvent)
        {
            var order = await _orderRepository.Get(domainEvent.OrderId);
            await _denormalizer.UpdatePaymentMethod(order);
        }
    }
}