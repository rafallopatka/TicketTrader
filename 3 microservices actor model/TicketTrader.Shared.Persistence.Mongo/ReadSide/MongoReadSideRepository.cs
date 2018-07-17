using System;
using MongoDB.Driver;
using TicketTrader.Shared.Base.CQRS.ReadModel;

namespace TicketTrader.Shared.Persistence.Mongo.ReadSide
{
    public abstract class MongoReadSideRepository<TReadModel> where TReadModel: IReadModel
    {
        protected IMongoDatabase Database { get; }
        protected IMongoCollection<TReadModel> Collection { get; }

        protected MongoReadSideRepository(MongoReadSideRepositoryContext context)
        {
            Database = context.Database;

            Collection = GetCollection();
        }

        private IMongoCollection<TReadModel> GetCollection()
        {
            var name = typeof(TReadModel).Name;
            return Database.GetCollection<TReadModel>(name);
        }
    }
}
