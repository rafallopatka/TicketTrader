using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using Akka.Persistence;
using TicketTrader.Orders.Domain.Commands;
using TicketTrader.Orders.Domain.Events;

// ReSharper disable UsePatternMatching

namespace TicketTrader.Orders.Domain.Actors
{
    class OrdersManagerActor : UntypedPersistentActor
    {
        private readonly IDictionary<string, IActorRef> _actorsToOrders;

        public override string PersistenceId { get; }

        public OrdersManagerActor()
        {
            PersistenceId = "OrdersManagerActor";
            _actorsToOrders = new Dictionary<string, IActorRef>();
        }

        protected override void OnCommand(object message)
        {
            switch (message)
            {
                case CreateNewOrder m:
                    {
                        var orderCreated = new OrderCreated(m.CustomerId, m.OrderId);
                        Context.PublishEvent(orderCreated);
                        Persist(orderCreated, CreateNewOrder);
                    }
                    break;
                case IOrderMessage m:
                    {
                        var actor = GetExistingOrderActor(m);

                        actor.Forward(m);
                    }
                    break;
                case Terminated m:
                    {
                        var msg = m;
                        var terminatedOrderId = _actorsToOrders.First(x => Equals(x.Value, m.ActorRef));
                        _actorsToOrders.Remove(terminatedOrderId);
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
                case OrderCreated m:
                    {
                        CreateNewOrder(m);
                    }
                    break;
                default:
                    break;
            }
        }

        protected void CreateNewOrder(OrderCreated message)
        {
            var newActor = Context.ActorOf(OrderActor.Props(message.OrderId, message.CustomerId));
            Context.Watch(newActor);
            _actorsToOrders[message.OrderId] = newActor;
        }

        protected IActorRef GetExistingOrderActor(IOrderMessage message)
        {
            if (_actorsToOrders.ContainsKey(message.OrderId))
            {
                return _actorsToOrders[message.OrderId];
            }

            return Context.ActorOf(OrderActor.Props(message.OrderId));
        }

        public static Props Props() => Akka.Actor.Props.Create<OrdersManagerActor>();
    }
}