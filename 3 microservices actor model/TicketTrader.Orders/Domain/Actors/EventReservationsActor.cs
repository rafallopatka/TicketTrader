using System.Linq;
using Akka.Actor;
using Akka.Persistence;
using TicketTrader.Orders.Domain.Commands;
using TicketTrader.Orders.Domain.Events;

namespace TicketTrader.Orders.Domain.Actors
{
    class EventReservationsActor : UntypedPersistentActor
    {
        public override string PersistenceId { get; }

        private EventReservationsState _state;

        public EventReservationsActor(string eventId)
        {
            PersistenceId = $"Reservations-{eventId}";
            _state = new EventReservationsState();
            _state.EventId = eventId;
        }

        protected override void OnCommand(object message)
        {
            switch (message)
            {
                case ReserveSeat m:
                    {
                        var reservationCreated = new ReservationCreated(m.CustomerId, m.OrderId, m.EventId, m.ReservationId, m.SceneSeatId);
                        Persist(reservationCreated, @event =>
                        {
                            ReserveSeat(@event);
                            SaveSnapshotIfNeeded();
                            Context.PublishEvent(@event);
                        });
                    }
                    break;
                case DiscardSeat m:
                    {
                        var reservationCreated = new ReservationDiscarded(m.OrderId, m.EventId, m.ReservationId);
                        Persist(reservationCreated, @event =>
                        {
                            DiscarReservation(@event);
                            SaveSnapshotIfNeeded();
                            Context.PublishEvent(@event);
                        });
                    }
                    break;
                case Terminated m:
                    {

                    }
                    break;
                default:
                    Unhandled(message);
                    break;
            }
        }

        protected override void OnRecover(object message)
        {
            switch (message)
            {
                case ReservationCreated m:
                    {
                        ReserveSeat(m);
                    }
                    break;
                case ReservationDiscarded m:
                    {
                        DiscarReservation(m);
                    }
                    break;
                case SnapshotOffer m:
                    {
                        var state = m.Snapshot as EventReservationsState;
                        _state = state;
                    }
                    break;
            }
        }

        private void ReserveSeat(ReservationCreated reservation)
        {
            _state.Reservations.Add(new Reservation
            {
                OrderId = reservation.OrderId,
                CustomerId = reservation.CustomerId,
                EventId = reservation.EventId
            });
        }


        private void DiscarReservation(ReservationDiscarded reservation)
        {
            var reservationToRemove = _state.Reservations.SingleOrDefault(x => x.ReservationId == reservation.ReservationId);
            if (reservationToRemove == null)
                return;

            _state.Reservations.Remove(reservationToRemove);
        }

        private int _eventsCount;
        private void SaveSnapshotIfNeeded()
        {
            _eventsCount++;

            if (_eventsCount == 5)
            {
                SaveSnapshot(_state);
                _eventsCount = 0;
            }
        }


        public static Props Props(string eventId) => Akka.Actor.Props.Create(() => new EventReservationsActor(eventId));
    }
}