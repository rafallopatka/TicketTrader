using Microsoft.Extensions.DependencyInjection;
using TicketTrader.Shared.Base.App;

namespace TicketTrader.Orders.Domain
{
    public static class AppActorsExtensions
    {
        public static App UseDomainActorSystem(this App app, string actorSystem, string journalConnectionString, string snapshotConnectionString)
        {
            app.Services.AddSingleton(provider => DomainActorSystemBuilder.Build(actorSystem, 
                journalConnectionString, 
                snapshotConnectionString));

            return app;
        }
    }
}