using System.Threading.Tasks;
using RawRabbit;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Shared.Bus.RabbitMq
{
    public class RabbitMqCommandBus : CommandBus
    {
        public override async Task DispatchAsync<TCommand>(TCommand command)
        {
            await Bus.Client.PublishAsync(command);
        }

        public override void Subscribe<TCommand>(ICommandHandler<TCommand> handler, string name = null)
        {
            Bus.Client.SubscribeAsync<TCommand>(async command => await handler.HandleAsync(command));
        }
    }
}
