using System;
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
    class CustomerOrdersByStateQueryHandler : UntypedPersistentActor
    {
        private State _state = new State();
        public override string PersistenceId { get; }

        public CustomerOrdersByStateQueryHandler(string customerId)
        {
            PersistenceId = $"{GetType().Name}-{customerId}";
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
                case GetCustomerOrdersByState m:
                    {
                        var result = _state.Orders.Where(x => x.State == m.State).ToList();

                        var response = new RespondCustomerOrdersByState(result);

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

            var order = new ClientOrderReadModel(@event.OrderId,
                @event.CustomerId,
                oldOrder.CreateDateTime,
                DateTime.Now,
                oldOrder.ExpirationTimeout,
                OrderStateReadModel.Canceled);

            _state.Orders.Replace(oldOrder, order);
        }

        private void Handle(OrderCreated @event)
        {
            var order = new ClientOrderReadModel(@event.OrderId,
                @event.CustomerId,
                DateTime.Now,
                DateTime.Now,
                TimeSpan.FromMinutes(15),
                OrderStateReadModel.Active);

            _state.Orders.Add(order);
        }

        private void Handle(OrderCompleted @event)
        {
            var oldOrder = _state.Orders.Single(x => x.Id == @event.OrderId);

            var order = new ClientOrderReadModel(@event.OrderId,
                @event.CustomerId,
                oldOrder.CreateDateTime,
                DateTime.Now,
                oldOrder.ExpirationTimeout,
                OrderStateReadModel.Finalized);

            _state.Orders.Replace(oldOrder, order);
        }

        private void Handle(OrderCommited @event)
        {
            var oldOrder = _state.Orders.Single(x => x.Id == @event.OrderId);

            var order = new ClientOrderReadModel(@event.OrderId,
                @event.CustomerId,
                oldOrder.CreateDateTime,
                DateTime.Now,
                oldOrder.ExpirationTimeout,
                OrderStateReadModel.Commited);

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
            public List<ClientOrderReadModel> Orders { get; set; } = new List<ClientOrderReadModel>();
        }

        public static Props Props(string customerId) => Akka.Actor.Props
            .Create(() => new CustomerOrdersByStateQueryHandler(customerId));
    }
}