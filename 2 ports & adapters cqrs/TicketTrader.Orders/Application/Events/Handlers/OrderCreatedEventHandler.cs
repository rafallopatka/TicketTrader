using System.Threading.Tasks;
using TicketTrader.Orders.Domain;
using TicketTrader.Orders.ReadModel;
using TicketTrader.Orders.ReadModel.Clients;
using TicketTrader.Orders.ReadModel.Orders;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Orders.Application.Events.Handlers
{
    public class OrderCreatedEventHandler : IEventHandler<OrderCreatedEvent>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IOrdersDenormalizer _ordersDenormalizer;
        private readonly IClientOrdersDenormalizer _clientOrdersDenormalizer;

        public OrderCreatedEventHandler(IRepository<Order> orderRepository, 
            IOrdersDenormalizer ordersDenormalizer,
            IClientOrdersDenormalizer clientOrdersDenormalizer)
        {
            _orderRepository = orderRepository;
            _ordersDenormalizer = ordersDenormalizer;
            _clientOrdersDenormalizer = clientOrdersDenormalizer;
        }

        public async Task Handle(OrderCreatedEvent domainEvent)
        {
            var order = await _orderRepository.Get(domainEvent.OrderId);
            await _ordersDenormalizer.CreateOrder(order);
            await _clientOrdersDenormalizer.CreateOrder(order);
        }
    }
}