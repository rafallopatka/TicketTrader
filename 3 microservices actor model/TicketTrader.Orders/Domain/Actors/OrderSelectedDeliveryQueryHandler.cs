using Akka.Actor;
using Akka.Persistence;
using TicketTrader.Orders.Domain.Events;
using TicketTrader.Orders.Domain.Queries;

namespace TicketTrader.Orders.Domain.Actors
{
    class OrderSelectedDeliveryQueryHandler : UntypedPersistentActor
    {
        private State _state = new State();
        public override string PersistenceId { get; }

        public OrderSelectedDeliveryQueryHandler(string orderId)
        {
            PersistenceId = $"{GetType().Name}-{orderId}";
            _state.OrderId = orderId;
        }

        protected override void OnCommand(object message)
        {
            switch (message)
            {
                case DeliveryMethodSet m:
                    {
                        Persist(m, @event =>
                        {
                            Handle(@event);
                            SaveSnapshot(_state);
                        });
                    }
                    break;
                case GetSelectedDelivery m:
                    {
                        var response = new RespondSelectedDelivery(_state.OrderId, _state.DeliveryTypeId);

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

        private void Handle(DeliveryMethodSet @event)
        {
            _state.DeliveryTypeId = @event.DeliveryMethodId;
        }

        protected override void OnRecover(object message)
        {
            switch (message)
            {
                case DeliveryMethodSet m:
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
            public string OrderId { get; set; }
            public string DeliveryTypeId { get; set; }
        }

        public static Props Props(string orderId) => Akka.Actor.Props
            .Create(() => new OrderSelectedDeliveryQueryHandler(orderId));
    }
}