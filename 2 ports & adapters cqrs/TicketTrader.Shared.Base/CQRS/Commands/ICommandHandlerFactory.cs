namespace TicketTrader.Shared.Base.CQRS.Commands
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<TCommand> Create<TCommand>() where TCommand : ICommand;
    }
}
