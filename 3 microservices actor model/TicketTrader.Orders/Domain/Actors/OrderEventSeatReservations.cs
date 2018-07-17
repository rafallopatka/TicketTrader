using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using Akka.Persistence;
using TicketTrader.Orders.Domain.Events;
using TicketTrader.Orders.Domain.Queries;
using TicketTrader.Orders.Domain.Queries.ReadModels;

namespace TicketTrader.Orders.Domain.Actors
{
    class OrderEventSeatReservations : UntypedPersistentActor
    {
        private State _state = new State();
        public override string PersistenceId { get; }

        public OrderEventSeatReservations(string orderId, string eventId)
        {
            PersistenceId = $"{GetType().Name}-{orderId}-{eventId}";
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
                case GetOrderEventSeatReservations m:
                    {
                        var result = _state.Reservations.Where(x => x.OrderId == m.OrderId && x.EventId == m.EventId)
                            .ToList();

                        var response = new RespondOrderEventSeatReservations(result);

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
            var reservationToRemove = _state.Reservations.Single(x => x.Id == @event.ReservationId);
            _state.Reservations.Remove(reservationToRemove);
        }

        private void Handle(ReservationCreated @event)
        {
            _state.Reservations.Add(new SeatReservationReadModel(@event.ReservationId, @event.SceneSeatId, @event.EventId, @event.OrderId));
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

        public static Props Props(string order, string eventId) => Akka.Actor.Props
            .Create(() => new OrderEventSeatReservations(order, eventId));
    }
}