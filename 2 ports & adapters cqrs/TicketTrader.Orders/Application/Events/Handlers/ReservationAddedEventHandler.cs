using System.Threading.Tasks;
using TicketTrader.Orders.Domain;
using TicketTrader.Orders.ReadModel.Reservations;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Orders.Application.Events.Handlers
{
    public class ReservationAddedEventHandler : IEventHandler<ReservationAddedEvent>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IOrderReservationDenormalizer _denormalizer;

        public ReservationAddedEventHandler(IRepository<Order> orderRepository, IOrderReservationDenormalizer denormalizer)
        {
            _orderRepository = orderRepository;
            _denormalizer = denormalizer;
        }

        public async Task Handle(ReservationAddedEvent domainEvent)
        {
            var order = await _orderRepository.Get(domainEvent.OrderId);
            var reservation = order.GetReservation(domainEvent.ReservationId);
            await _denormalizer.CreateReservation(order, reservation);
        }
    }
}