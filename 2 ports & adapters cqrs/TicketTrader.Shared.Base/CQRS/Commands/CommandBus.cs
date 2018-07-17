using System;
using System.Threading.Tasks;

namespace TicketTrader.Shared.Base.CQRS.Commands
{
    public abstract class CommandBus
    {
        public abstract Task DispatchAsync<TCommand>(TCommand command) where TCommand: ICommand;
        public abstract void Subscribe<TCommand>(ICommandHandler<TCommand> handler, String name = null) where TCommand : ICommand;

        public static CommandBus Current { get; set; }
    }
}
