using System;
using MongoDB.Driver;

namespace TicketTrader.Shared.Persistence.Mongo.Domain
{
    internal class MongoDomainRepositoryContext
    {
        private readonly Lazy<IMongoDatabase> _db;

        public MongoDomainRepositoryContext(string connectionString, string database)
        {
            _db = new Lazy<IMongoDatabase>(() =>
            {
                var c = new MongoClient(connectionString);
                IMongoDatabase db = c.GetDatabase(database);

                return db;
            });
        }

        public IMongoDatabase Database => _db.Value;
    }
}