using System.Threading.Tasks;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Shared.Test
{
    public class TestCommandBus : CommandBus
    {
        public override async Task DispatchAsync<TCommand>(TCommand command)
        {
        }

        public override void Subscribe<TCommand>(ICommandHandler<TCommand> handler, string name = null)
        {
        }
    }
}
