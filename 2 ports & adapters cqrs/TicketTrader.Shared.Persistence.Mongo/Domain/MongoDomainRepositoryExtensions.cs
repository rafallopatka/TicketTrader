using System;
using Microsoft.Extensions.DependencyInjection;
using TicketTrader.Shared.Base;
using TicketTrader.Shared.Base.App;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Shared.Persistence.Mongo.Domain
{
    public static class MongoDomainRepositoryExtensions
    {
        public static App UseMongoOnWriteSide(this App app, string connectionString, string database)
        {
            app.Services.AddTransient(provider => new MongoDomainRepositoryContext(connectionString, database));
            app.Services.AddTransient(typeof(IRepository<>), typeof(MongoDomainRepository<>));

            return app;
        }
        public static App.AppHost InitializeMongoDomainRepository(this App.AppHost host)
        {
            var ctx = host.ServiceProvider.GetService<MongoDomainRepositoryContext>();
            var db = ctx.Database;
            Console.WriteLine($"Db initialized {db.DatabaseNamespace}");

            return host;
        }
    }
}
