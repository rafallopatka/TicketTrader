using System.Threading.Tasks;

namespace TicketTrader.Shared.Base.CQRS.Commands
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);
    }
}
