using System.Threading.Tasks;
using TicketTrader.Orders.Domain;
using TicketTrader.Orders.ReadModel;
using TicketTrader.Orders.ReadModel.Clients;
using TicketTrader.Orders.ReadModel.Tickets;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Orders.Application.Events.Handlers
{
    public class TicketRemovedEventHandler : IEventHandler<TicketRemovedEvent>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IOrderTicketsDenormalizer _denormalizer;

        public TicketRemovedEventHandler(IRepository<Order> orderRepository, IOrderTicketsDenormalizer denormalizer)
        {
            _orderRepository = orderRepository;
            _denormalizer = denormalizer;
        }

        public async Task Handle(TicketRemovedEvent domainEvent)
        {
            var order = await _orderRepository.Get(domainEvent.OrderId);
            var ticket = order.GetTicket(domainEvent.TicketId);
            await _denormalizer.RemoveTicket(ticket);
        }
    }
}