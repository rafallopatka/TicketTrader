using System.Linq;
using Akka.Actor;
using Akka.Persistence;
using TicketTrader.Orders.Domain.Commands;
using TicketTrader.Orders.Domain.Events;

namespace TicketTrader.Orders.Domain.Actors
{
    class OrderActor : UntypedPersistentActor
    {
        private OrderState _state;

        public override string PersistenceId { get; }

        public OrderActor(string orderId)
        {
            PersistenceId = $"OrderActor_{orderId}";

            _state = new OrderState();
        }

        public OrderActor(string orderId, string customerId)
        {
            PersistenceId = $"OrderActor_{orderId}";

            _state = new OrderState
            {
                OrderId = orderId,
                CustomerId = customerId,
                Status = OrderStatus.New,
            };
        }

        private int _eventsCount = 0;
        private void SaveSnapshotIfNeeded()
        {
            _eventsCount++;

            if (_eventsCount == 5)
            {
                SaveSnapshot(_state);
                _eventsCount = 0;
            }
        }

        protected override void OnCommand(object message)
        {
            switch (message)
            {
                case SetDelivery m:
                    {
                        var deliverySet = new DeliveryMethodSet(m.OrderId, m.DeliveryTypeId);
                        Persist(deliverySet, @event =>
                        {
                            SetDelivery(@event);
                            SaveSnapshotIfNeeded();
                            Context.PublishEvent(@event);
                        });
                    }
                    break;
                case SetPayment m:
                    {
                        var paymentSet = new PaymentMethodSet(m.OrderId, m.PaymentTypeId);
                        Persist(paymentSet, @event =>
                        {
                            SetPayment(@event);
                            SaveSnapshotIfNeeded();
                            Context.PublishEvent(@event);
                        });
                    }
                    break;
                case DiscardOrder m:
                    {
                        var orderDiscarded = new OrderDiscarded(m.OrderId, _state.CustomerId);
                        Persist(orderDiscarded, @event =>
                        {
                            DiscardOrder(@event);
                            SaveSnapshotIfNeeded();
                            Context.PublishEvent(@event);
                        });
                    }
                    break;
                case CommitOrder m:
                    {
                        var orderCommited = new OrderCommited(m.OrderId, _state.CustomerId);
                        Persist(orderCommited, @event =>
                        {
                            CommitOrder(@event);
                            SaveSnapshotIfNeeded();
                            Context.PublishEvent(@event);
                        });
                    }
                    break;
                case CompleteOrder m:
                    {
                        var orderCompleted = new OrderCompleted(m.OrderId, _state.CustomerId);
                        Persist(orderCompleted, @event =>
                        {
                            CompleteOrder(@event);
                            SaveSnapshotIfNeeded();
                            Context.PublishEvent(@event);
                        });
                    }
                    break;
                case ReserveTicket m:
                    {
                        var ticketReserved = new TicketReserved(
                            m.OrderId,
                            m.CustomerId,
                            m.TicketId,
                            m.EventId,
                            m.PriceOptionId,
                            m.SceneSeatId,
                            m.PriceZoneName,
                            m.PriceOptionName,
                            m.GrossAmount);

                        Persist(ticketReserved, @event =>
                        {
                            ReserveTicket(@event);
                            SaveSnapshotIfNeeded();
                            Context.PublishEvent(@event);
                        });
                    }
                    break;

                case RemoveTicket m:
                    {
                        var ticketRemoved = new TicketRemoved(m.OrderId, m.CustomerId, m.EventId, m.TicketId);

                        Persist(ticketRemoved, @event =>
                        {
                            RemoveTicket(@event);
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
                case DeliveryMethodSet m:
                    {
                        SetDelivery(m);
                    }
                    break;
                case PaymentMethodSet m:
                    {
                        SetPayment(m);
                    }
                    break;
                case OrderDiscarded m:
                    {
                        DiscardOrder(m);
                    }
                    break;
                case OrderCommited m:
                    {
                        CommitOrder(m);
                    }
                    break;
                case OrderCompleted m:
                    {
                        CompleteOrder(m);
                    }
                    break;

                case TicketReserved m:
                    {
                        ReserveTicket(m);
                    }
                    break;
                case TicketRemoved m:
                    {
                        RemoveTicket(m);
                    }
                    break;
                case SnapshotOffer m:
                    {
                        var state = m.Snapshot as OrderState;
                        _state = state;
                    }
                    break;
            }
        }

        private void ReserveTicket(TicketReserved m)
        {
            _state.Tickets.Add(new Ticket
            {
                Id = m.TicketId,
            });
        }

        private void RemoveTicket(TicketRemoved m)
        {
            var ticketToRemove = _state.Tickets.SingleOrDefault(x => x.Id == m.TicketId);
            if (ticketToRemove == null)
                return;

            _state.Tickets.Remove(ticketToRemove);
        }

        private void SetDelivery(DeliveryMethodSet m)
        {
            _state.DeliveryMethodId = m.DeliveryMethodId;
        }

        private void SetPayment(PaymentMethodSet m)
        {
            _state.PaymentMethodId = m.PaymentTypeId;
        }

        private void DiscardOrder(OrderDiscarded m)
        {
            _state.Status = OrderStatus.Discarded;
        }

        private void CommitOrder(OrderCommited m)
        {
            _state.Status = OrderStatus.Commited;
        }

        private void CompleteOrder(OrderCompleted m)
        {
            _state.Status = OrderStatus.Completed;
        }
        public static Props Props(string orderId) => Akka.Actor.Props.Create(() => new OrderActor(orderId));
        public static Props Props(string orderId, string customerId) => Akka.Actor.Props.Create(() => new OrderActor(orderId, customerId));
    }
}