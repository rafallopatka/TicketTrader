using System;
using Microsoft.Extensions.DependencyInjection;
using TicketTrader.Shared.Base.App;

namespace TicketTrader.Shared.Persistence.Mongo.ReadSide
{
    public static class MongoReadRepositoryExtensions
    {
        public static App UseMongoOnReadSide(this App app, string connectionString, string database)
        {
            app.Services.AddTransient(provider => new MongoReadSideRepositoryContext(connectionString, database));

            return app;
        }
        public static App.AppHost InitializeMongoReadRepository(this App.AppHost host)
        {
            var ctx = host.ServiceProvider.GetService<MongoReadSideRepositoryContext>();
            var db = ctx.Database;
            Console.WriteLine($"Db read initialized {db.DatabaseNamespace}");

            return host;
        }
    }
}
