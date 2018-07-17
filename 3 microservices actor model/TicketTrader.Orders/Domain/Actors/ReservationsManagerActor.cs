using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using TicketTrader.Orders.Domain.Commands;

namespace TicketTrader.Orders.Domain.Actors
{
    class ReservationsManagerActor : UntypedActor
    {
        private readonly IDictionary<string, IActorRef> _actors;

        public ReservationsManagerActor()
        {
            _actors = new Dictionary<string, IActorRef>();
        }

        protected override void OnReceive(object message)
        {
            switch (message)
            {
                case ReserveSeat m:
                    {
                        var actor = GetEventReservationActor(m.EventId);
                        actor.Forward(m);
                    }
                    break;
                case DiscardSeat m:
                    {
                        var actor = GetEventReservationActor(m.EventId);
                        actor.Forward(m);
                    }
                    break;
                case Terminated m:
                    {
                        var terminatedOrderId = _actors.First(x => Equals(x.Value, m.ActorRef));
                        _actors.Remove(terminatedOrderId);
                    }
                    break;
                default:
                    Unhandled(message);
                    break;
            }
        }


        protected IActorRef GetEventReservationActor(string eventId)
        {
            if (_actors.ContainsKey(eventId) == false)
            {
                var newActor = Context.ActorOf(EventReservationsActor.Props(eventId));
                Context.Watch(newActor);
                _actors[eventId] = newActor;
                return newActor;
            }

            return _actors[eventId];
        }

        public static Props Props() => Akka.Actor.Props.Create<ReservationsManagerActor>();
    }
}