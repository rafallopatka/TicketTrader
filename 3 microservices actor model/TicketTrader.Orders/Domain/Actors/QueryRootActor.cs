using System;
using Akka.Actor;
using Akka.Routing;
using TicketTrader.Orders.Domain.Queries;

namespace TicketTrader.Orders.Domain.Actors
{
    internal class QueryRootActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            switch (message)
            {
                case GetAwaitingOrders m:
                    {
                        var actor = Context.ActorOf(AwaitingOrdersQueryHandler.Props());
                        actor.Forward(m);
                    }
                    break;
                case GetCustomerOrdersByState m:
                    {
                        var actor = Context.ActorOf(CustomerOrdersByStateQueryHandler.Props(m.CustomerId));
                        actor.Forward(m);
                    }
                    break;
                case GetCustomerTickets m:
                    {
                        var actor = Context.ActorOf(CustomerTicketsQueryHandler.Props(m.CustomerId, m.OrderId));
                        actor.Forward(m);
                    }
                    break;
                case GetCustomerTicketsForEvent m:
                    {
                        var actor = Context.ActorOf(CustomerTicketsForEventQueryHandler.Props(m.CustomerId, m.EventId));
                        actor.Forward(m);
                    }
                    break;
                case GetOrderEventSeatReservations m:
                    {
                        var actor = Context.ActorOf(OrderEventSeatReservations.Props(m.OrderId, m.EventId));
                        actor.Forward(m);
                    }
                    break;
                case GetAllEventSeatReservations m:
                    {
                        var actor = Context.ActorOf(EventSeatReservationsQueryHandler.Props(m.EventId));
                        actor.Forward(m);
                    }
                    break;
                case GetSelectedDelivery m:
                    {
                        var actor = Context.ActorOf(OrderSelectedDeliveryQueryHandler.Props(m.OrderId));
                        actor.Forward(m);
                    }
                    break;
                case GetSelectedPayment m:
                    {
                        var actor = Context.ActorOf(OrderSelectedPaymentQueryHandler.Props(m.OrderId));
                        actor.Forward(m);
                    }
                    break;
                case Terminated m:
                    {

                    }
                    break;
                default:
                    {
                        Unhandled(message);
                    }
                    break;
            }
        }

        public static Props Props() => Akka.Actor.Props
            .Create<QueryRootActor>();
    }
}