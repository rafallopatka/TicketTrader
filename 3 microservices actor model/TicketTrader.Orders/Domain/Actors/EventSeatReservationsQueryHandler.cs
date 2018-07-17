using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using Akka.Persistence;
using TicketTrader.Orders.Domain.Events;
using TicketTrader.Orders.Domain.Queries;
using TicketTrader.Orders.Domain.Queries.ReadModels;

namespace TicketTrader.Orders.Domain.Actors
{
    class EventSeatReservationsQueryHandler : UntypedPersistentActor
    {
        public override string PersistenceId { get; }
        private State _state = new State();

        public EventSeatReservationsQueryHandler(string eventId)
        {
            PersistenceId = $"{GetType().Name}-{eventId}";
        }

        protected override void OnCommand(object message)
        {
            switch (message)
            {
                case ReservationCreated m:
                {
                    Persist(m, @event =>
                    {
                        Handle(@event);
                        SaveSnapshot(_state);
                    });
                }
                    break;
                case ReservationDiscarded m:
                {
                    Persist(m, @event =>
                    {
                        Handle(@event);
                        SaveSnapshot(_state);
                    });
                }
                    break;
                case GetAllEventSeatReservations m:
                {
                    var reservations = _state.Reservations.Where(x => x.EventId == m.EventId).ToList();

                    var response = new RespondAllEventSeatReservations(reservations);

                    Sender.Tell(response);
                }
                    break;
                default:
                {
                    Unhandled(message);
                }
                    break;
            }
        }

        private void Handle(ReservationDiscarded @event)
        {
            var reservation = _state.Reservations.Single(x => x.Id == @event.ReservationId);
            _state.Reservations.Remove(reservation);
        }

        private void Handle(ReservationCreated @event)
        {
            var reservation = new SeatReservationReadModel(@event.ReservationId, @event.SceneSeatId, @event.EventId, @event.OrderId);

            _state.Reservations.Add(reservation);
        }

        protected override void OnRecover(object message)
        {
            switch (message)
            {
                case ReservationCreated m:
                {
                    Handle(m);
                }
                    break;
                case ReservationDiscarded m:
                {
                    Handle(m);
                }
                    break;
                case SnapshotOffer m:
                {
                    var state = m.Snapshot as State;
                    _state = state;
                }
                    break;
            }
        }

        internal class State
        {
            public List<SeatReservationReadModel> Reservations { get; set; } = new List<SeatReservationReadModel>();
        }

        public static Props Props(string eventId) => Akka.Actor.Props
            .Create(() => new EventSeatReservationsQueryHandler(eventId));
    }
}