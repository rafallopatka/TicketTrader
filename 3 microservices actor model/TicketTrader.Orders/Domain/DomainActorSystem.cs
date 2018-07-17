using System;
using System.Threading.Tasks;
using Akka.Actor;
using TicketTrader.Orders.Domain.Actors;
using TicketTrader.Orders.Domain.Commands;
using TicketTrader.Orders.Domain.Events;
using TicketTrader.Orders.Domain.Queries;

namespace TicketTrader.Orders.Domain
{
    public class DomainActorSystem : IDisposable
    {
        public string ActorSystemName { get; }
        private readonly ActorSystem _actorSystem;
        private readonly IActorRef _commandRoot;
        private readonly IActorRef _eventRoot;
        private readonly IActorRef _queryRoot;

        public DomainActorSystem(string actorSystemName, ActorSystem actorSystem)
        {
            ActorSystemName = actorSystemName;
            _actorSystem = actorSystem;

            _commandRoot = _actorSystem.ActorOf(CommandRootActor.Props(), "command-root");
            _eventRoot = _actorSystem.ActorOf(EventRootActor.Props(), "event-root");
            _queryRoot = _actorSystem.ActorOf(QueryRootActor.Props(), "query-root");
        }

        public void Execute(ICommandMessage command)
        {
            _commandRoot.Tell(command);
        }

        public async Task<TResult> Query<TResult>(IQueryMessage query)
        {
            return await _queryRoot.Ask<TResult>(query);
        }

        public void React(IEventMessage evt)
        {
            _eventRoot.Tell(evt);
        }

        public void Dispose()
        {
            _actorSystem?.Dispose();
        }
    }

    public static class EventActorSystemExtensions
    {
        public static void PublishEvent(this IActorContext @this, IEventMessage message)
        {
            @this.System.ActorSelection("*/event-root").Tell(message);
        }
    }
}