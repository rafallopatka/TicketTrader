using System.Threading.Tasks;
using TicketTrader.Orders.Domain;
using TicketTrader.Orders.ReadModel.Tickets;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Orders.Application.Events.Handlers
{
    public class TicketAddedEventHandler : IEventHandler<TicketAddedEvent>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IOrderTicketsDenormalizer _denormalizer;

        public TicketAddedEventHandler(IRepository<Order> orderRepository, IOrderTicketsDenormalizer denormalizer)
        {
            _orderRepository = orderRepository;
            _denormalizer = denormalizer;
        }

        public async Task Handle(TicketAddedEvent domainEvent)
        {
            var order = await _orderRepository.Get(domainEvent.OrderId);
            var ticket = order.GetTicket(domainEvent.TicketId);
            await _denormalizer.CreateTicket(order, ticket);
        }
    }
}