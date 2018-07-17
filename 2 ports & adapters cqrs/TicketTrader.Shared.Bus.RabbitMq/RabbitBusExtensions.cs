using TicketTrader.Shared.Base;
using TicketTrader.Shared.Base.App;
using TicketTrader.Shared.Base.CQRS.Commands;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Shared.Bus.RabbitMq
{
    public static class RabbitBusExtensions
    {
        public static App UseRabbitMqBus(this App app)
        {
            CommandBus.Current = new RabbitMqCommandBus();
            EventBus.Current = new RabbitMqEventBus();
            QueryBus.Current = new RabbitMqQueryBus();

            return app;
        }
    }
}
