using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using Akka.Persistence;
using TicketTrader.Orders.Domain.Events;
using TicketTrader.Orders.Domain.Queries;
using TicketTrader.Orders.Domain.Queries.ReadModels;

namespace TicketTrader.Orders.Domain.Actors
{
    class CustomerTicketsQueryHandler : UntypedPersistentActor
    {
        private State _state = new State();
        public override string PersistenceId { get; }

        public CustomerTicketsQueryHandler(string customerId, string orderId)
        {
            PersistenceId = $"{GetType().Name}-{customerId}-{orderId}";
        }

        protected override void OnCommand(object message)
        {
            switch (message)
            {
                case TicketReserved m:
                {
                    Persist(m, @event =>
                    {
                        Handle(@event);
                        SaveSnapshot(_state);
                    });
                }
                    break;
                case TicketRemoved m:
                {
                    Persist(m, @event =>
                    {
                        Handle(@event);
                        SaveSnapshot(_state);
                    });
                }
                    break;
                case GetCustomerTickets m:
                {
                    var result = _state.Tickets.Where(x => x.OrderId == m.OrderId).ToList();

                    var response = new RespondCustomerTickets(result);

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

        private void Handle(TicketRemoved @event)
        {
            var ticketToRemove = _state.Tickets.Single(x => x.Id == @event.TicketId);
            _state.Tickets.Remove(ticketToRemove);
        }

        private void Handle(TicketReserved @event)
        {
            _state.Tickets.Add(new OrderTicketReadModel(@event.TicketId,
                @event.EventId,
                @event.CustomerId,
                @event.PriceOptionId,
                @event.OrderId,
                @event.PriceZoneName,
                @event.PriceOptionName,
                @event.GrossAmount,
                new[]
                {
                    @event.SceneSeatId
                }));
        }

        protected override void OnRecover(object message)
        {
            switch (message)
            {
                case TicketReserved m:
                {
                    Handle(m);
                }
                    break;
                case TicketRemoved m:
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
            public List<OrderTicketReadModel> Tickets { get; set; } = new List<OrderTicketReadModel>();
        }

        public static Props Props(string customerId, string orderId) => Akka.Actor.Props
            .Create(() => new CustomerTicketsQueryHandler(customerId, orderId));
    }
}