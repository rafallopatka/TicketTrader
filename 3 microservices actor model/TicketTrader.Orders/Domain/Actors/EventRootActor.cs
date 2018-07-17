using Akka.Actor;
using Akka.Routing;
using TicketTrader.Orders.Domain.Events;

namespace TicketTrader.Orders.Domain.Actors
{
    class EventRootActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            switch (message)
            {
                case OrderCommited m:
                    {
                        Context.ActorOf(AwaitingOrdersQueryHandler.Props()).Forward(m);
                        Context.ActorOf(CustomerOrdersByStateQueryHandler.Props(m.CustomerId)).Forward(m);
                    }
                    break;
                case OrderCompleted m:
                    {
                        Context.ActorOf(AwaitingOrdersQueryHandler.Props()).Forward(m);
                        Context.ActorOf(CustomerOrdersByStateQueryHandler.Props(m.CustomerId)).Forward(m);
                    }
                    break;
                case OrderCreated m:
                    {
                        Context.ActorOf(AwaitingOrdersQueryHandler.Props()).Forward(m);
                        Context.ActorOf(CustomerOrdersByStateQueryHandler.Props(m.CustomerId)).Forward(m);
                    }
                    break;
                case OrderDiscarded m:
                    {
                        Context.ActorOf(AwaitingOrdersQueryHandler.Props()).Forward(m);
                        Context.ActorOf(CustomerOrdersByStateQueryHandler.Props(m.CustomerId)).Forward(m);
                    }
                    break;
                case DeliveryMethodSet m:
                    {
                        Context.ActorOf(OrderSelectedDeliveryQueryHandler.Props(m.OrderId)).Forward(m);
                    }
                    break;

                case PaymentMethodSet m:
                    {
                        Context.ActorOf(OrderSelectedPaymentQueryHandler.Props(m.OrderId)).Forward(m);
                    }
                    break;
                case ReservationCreated m:
                    {
                        Context.ActorOf(OrderEventSeatReservations.Props(m.OrderId, m.EventId)).Forward(m);
                        Context.ActorOf(EventSeatReservationsQueryHandler.Props(m.EventId)).Forward(m);
                    }
                    break;
                case ReservationDiscarded m:
                    {
                        Context.ActorOf(OrderEventSeatReservations.Props(m.OrderId, m.EventId)).Forward(m);
                        Context.ActorOf(EventSeatReservationsQueryHandler.Props(m.EventId)).Forward(m);
                    }
                    break;
                case TicketReserved m:
                    {
                        Context.ActorOf(CustomerTicketsQueryHandler.Props(m.CustomerId, m.OrderId)).Forward(m);
                        Context.ActorOf(CustomerTicketsForEventQueryHandler.Props(m.CustomerId, m.EventId)).Forward(m);
                    }
                    break;
                case TicketRemoved m:
                    {
                        Context.ActorOf(CustomerTicketsQueryHandler.Props(m.CustomerId, m.OrderId)).Forward(m);
                        Context.ActorOf(CustomerTicketsForEventQueryHandler.Props(m.CustomerId, m.EventId)).Forward(m);
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

        public static Props Props() => Akka.Actor.Props.Create<EventRootActor>();
    }
}