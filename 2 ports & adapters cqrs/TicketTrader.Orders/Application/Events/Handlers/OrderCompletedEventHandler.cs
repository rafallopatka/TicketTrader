using System.Threading.Tasks;
using TicketTrader.Orders.Domain;
using TicketTrader.Orders.ReadModel;
using TicketTrader.Orders.ReadModel.Clients;
using TicketTrader.Orders.ReadModel.Orders;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Orders.Application.Events.Handlers
{
    public class OrderCompletedEventHandler : IEventHandler<OrderCompletedEvent>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IOrdersDenormalizer _denormalizer;
        private readonly IClientOrdersDenormalizer _ordersDenormalizer;

        public OrderCompletedEventHandler(IRepository<Order> orderRepository, 
            IOrdersDenormalizer denormalizer,
            IClientOrdersDenormalizer ordersDenormalizer)
        {
            _orderRepository = orderRepository;
            _denormalizer = denormalizer;
            _ordersDenormalizer = ordersDenormalizer;
        }

        public async Task Handle(OrderCompletedEvent domainEvent)
        {
            var order = await _orderRepository.Get(domainEvent.OrderId);
            await _denormalizer.UpdateOrderStatus(order);
            await _ordersDenormalizer.UpdateOrder(order);
        }
    }
}
