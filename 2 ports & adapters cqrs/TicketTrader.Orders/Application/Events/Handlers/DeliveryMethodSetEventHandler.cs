using System.Threading.Tasks;
using TicketTrader.Orders.Domain;
using TicketTrader.Orders.ReadModel.Deliveries;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Orders.Application.Events.Handlers
{
    public class DeliveryMethodSetEventHandler : IEventHandler<DeliveryMethodSetEvent>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IOrderDeliveriesDenormalizer _denormalizer;

        public DeliveryMethodSetEventHandler(IRepository<Order> orderRepository, IOrderDeliveriesDenormalizer denormalizer)
        {
            _orderRepository = orderRepository;
            _denormalizer = denormalizer;
        }

        public async Task Handle(DeliveryMethodSetEvent domainEvent)
        {
            var order = await _orderRepository.Get(domainEvent.OrderId);
            await _denormalizer.UpdateDeliveryMethod(order);
        }
    }
}