using Akka.Actor;
using Akka.Persistence;
using TicketTrader.Orders.Domain.Events;
using TicketTrader.Orders.Domain.Queries;

namespace TicketTrader.Orders.Domain.Actors
{
    class OrderSelectedPaymentQueryHandler : UntypedPersistentActor
    {
        private State _state = new State();
        public override string PersistenceId { get; }

        public OrderSelectedPaymentQueryHandler(string orderId)
        {
            PersistenceId = $"{GetType().Name}-{orderId}";
            _state.OrderId = orderId;
        }

        protected override void OnCommand(object message)
        {
            switch (message)
            {
                case PaymentMethodSet m:
                {
                    Persist(m, @event =>
                    {
                        Handle(@event);
                        SaveSnapshot(_state);
                    });
                }
                    break;
                case GetSelectedPayment m:
                {
                    var response = new RespondSelectedPayment(_state.OrderId, _state.PaymentTypeId);

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

        private void Handle(PaymentMethodSet @event)
        {
            _state.PaymentTypeId = @event.PaymentTypeId;
        }

        protected override void OnRecover(object message)
        {
            switch (message)
            {
                case PaymentMethodSet m:
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
            public string PaymentTypeId { get; set; }
        }

        public static Props Props(string orderId) => Akka.Actor.Props
            .Create(() => new OrderSelectedPaymentQueryHandler(orderId));
    }
}