using System.Threading.Tasks;
using TicketTrader.Orders.Canonical.Commands;
using TicketTrader.Orders.Domain;
using TicketTrader.Shared.Base.CQRS.Commands;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Orders.Application.Commands.Handlers
{
    class ReserveSeatCommandHandler: ICommandHandler<ReserveSeatCommand>
    {
        private readonly IRepository<Order> _orderRepository;

        public ReserveSeatCommandHandler(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task HandleAsync(ReserveSeatCommand command)
        {
            var order = await _orderRepository.Get(Id.From(command.OrderId));

            var eventId = Id.From(command.EventId);
            var seatId = Id.From(command.SceneSeatId);
            var reservationId = Id.From(command.ReservationId);

            var reservation = new Reservation(reservationId, eventId, new Seat(seatId));

            order.AddReservation(reservation);
            await _orderRepository.Save(order);

            await EventBus.Current.PublishAsync(new ReservationAddedEvent(order.Id, reservation.Id));
        }
    }
}