using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using Akka.Persistence;
using TicketTrader.Orders.Domain.Events;
using TicketTrader.Orders.Domain.Queries;
using TicketTrader.Orders.Domain.Queries.ReadModels;
using TicketTrader.Shared.Base.Infrastructure;

namespace TicketTrader.Orders.Domain.Actors
{
    class AwaitingOrdersQueryHandler : UntypedPersistentActor
    {
        public override string PersistenceId { get; }
        private State _state = new State();

        public AwaitingOrdersQueryHandler()
        {
            PersistenceId = $"{GetType().Name}";
        }

        protected override void OnCommand(object message)
        {
            switch (message)
            {
                case OrderCommited m:
                    {
                        Persist(m, @event =>
                        {
                            Handle(@event);
                            SaveSnapshot(_state);
                        });
                    }
                    break;
                case OrderCompleted m:
                    {
                        Persist(m, @event =>
                        {
                            Handle(@event);
                            SaveSnapshot(_state);
                        });
                    }
                    break;
                case OrderCreated m:
                    {
                        Persist(m, @event =>
                        {
                            Handle(@event);
                            SaveSnapshot(_state);
                        });
                    }
                    break;
                case OrderDiscarded m:
                    {
                        Persist(m, @event =>
                        {
                            Handle(@event);
                            SaveSnapshot(_state);
                        });
                    }
                    break;

                case GetAwaitingOrders m:
                    {
                        var result = _state.Orders
                            .Where(x => x.State == OrderStateReadModel.Commited)
                            .ToList();

                        var response = new RespondAwaitingOrders(result);

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

        private void Handle(OrderDiscarded @event)
        {
            var oldOrder = _state.Orders.Single(x => x.Id == @event.OrderId);
            var order = new OrderReadModel(@event.OrderId, OrderStateReadModel.Canceled);
            _state.Orders.Replace(oldOrder, order);
        }

        private void Handle(OrderCreated @event)
        {
            _state.Orders.Add(new OrderReadModel(@event.OrderId, OrderStateReadModel.Active));
        }

        private void Handle(OrderCompleted @event)
        {
            var oldOrder = _state.Orders.Single(x => x.Id == @event.OrderId);
            var order = new OrderReadModel(@event.OrderId, OrderStateReadModel.Finalized);
            _state.Orders.Replace(oldOrder, order);
        }

        private void Handle(OrderCommited @event)
        {
            var oldOrder = _state.Orders.Single(x => x.Id == @event.OrderId);
            var order = new OrderReadModel(@event.OrderId, OrderStateReadModel.Commited);
            _state.Orders.Replace(oldOrder, order);
        }

        protected override void OnRecover(object message)
        {
            switch (message)
            {
                case OrderCommited m:
                    {
                        Handle(m);
                    }
                    break;
                case OrderCompleted m:
                    {
                        Handle(m);
                    }
                    break;
                case OrderCreated m:
                    {
                        Handle(m);
                    }
                    break;
                case OrderDiscarded m:
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
            public List<OrderReadModel> Orders { get; set; } = new List<OrderReadModel>();
        }

        public static Props Props() => Akka.Actor.Props
            .Create(() => new AwaitingOrdersQueryHandler());
    }
}