
using System.Threading.Tasks;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Shared.CommandBus
{
    public class InMemoryCommandBus: ICommandBus
    {
        public async Task DispatchAsync(ICommand command)
        {
        }
    }
}
