using Akka.Actor;
using Akka.Routing;
using TicketTrader.Orders.Domain.Commands;

namespace TicketTrader.Orders.Domain.Actors
{
    class CommandRootActor : UntypedActor
    {
        private IActorRef _orderManagerActor;
        private IActorRef _reservations;

        protected override void PreStart()
        {
            base.PreStart();
            _orderManagerActor = Context.ActorOf(OrdersManagerActor.Props(), "order-manager");
            _reservations = Context.ActorOf(ReservationsManagerActor.Props(), "reservations");
        }

        protected override void OnReceive(object message)
        {
            switch (message)
            {
                case CreateNewOrder m:
                    {
                        _orderManagerActor.Forward(m);
                    }
                    break;
                case SetDelivery m:
                    {
                        _orderManagerActor.Forward(m);
                    }
                    break;
                case SetPayment m:
                    {
                        _orderManagerActor.Forward(m);
                    }
                    break;
                case DiscardOrder m:
                    {
                        _orderManagerActor.Forward(m);
                    }
                    break;
                case CommitOrder m:
                    {
                        _orderManagerActor.Forward(m);
                    }
                    break;
                case CompleteOrder m:
                    {
                        _orderManagerActor.Forward(m);
                    }
                    break;
                case ReserveTicket m:
                    {
                        _orderManagerActor.Forward(m);
                    }
                    break;
                case RemoveTicket m:
                    {
                        _orderManagerActor.Forward(m);
                    }
                    break;
                case ReserveSeat m:
                    {
                        _reservations.Forward(m);
                    }
                    break;
                case DiscardSeat m:
                    {
                        _reservations.Forward(m);
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

        public static Props Props() => Akka.Actor.Props
            .Create<CommandRootActor>();
    }
}