using System;
using MongoDB.Driver;

namespace TicketTrader.Shared.Persistence.Mongo.ReadSide
{
    public class MongoReadSideRepositoryContext
    {
        private readonly Lazy<IMongoDatabase> _db;

        public MongoReadSideRepositoryContext(string connectionString, string database)
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