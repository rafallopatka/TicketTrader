using System.Threading.Tasks;
using TicketTrader.Orders.Domain;
using TicketTrader.Orders.ReadModel;
using TicketTrader.Orders.ReadModel.Clients;
using TicketTrader.Orders.ReadModel.Reservations;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Orders.Application.Events.Handlers
{
    public class ReservationDiscardedEventHandler : IEventHandler<ReservationDiscardedEvent>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IOrderReservationDenormalizer _denormalizer;

        public ReservationDiscardedEventHandler(IRepository<Order> orderRepository, IOrderReservationDenormalizer denormalizer)
        {
            _orderRepository = orderRepository;
            _denormalizer = denormalizer;
        }

        public async Task Handle(ReservationDiscardedEvent domainEvent)
        {
            var order = await _orderRepository.Get(domainEvent.OrderId);
            var reservation = order.GetReservation(domainEvent.ReservationId);
            await _denormalizer.DiscardReservation(reservation);
        }
    }
}