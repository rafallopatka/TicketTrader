using System.Threading.Tasks;
using MongoDB.Driver;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Shared.Persistence.Mongo.Domain
{
    internal class MongoDomainRepository<TDomainEntity> : Repository<TDomainEntity>, IRepository<TDomainEntity> where TDomainEntity : DomainEntity
    {
        private readonly IMongoDatabase _db;
        private readonly IMongoCollection<TDomainEntity> _collection;

        public MongoDomainRepository(MongoDomainRepositoryContext context)
        {
            _db = context.Database;
            _collection = GetCollection();
        }


        public override async Task<TDomainEntity> Get(Id id)
        {
            return await _collection
                .Find(entity => Equals(entity.Id, id))
                .FirstOrDefaultAsync();
        }

        public override async Task Save(TDomainEntity entity)
        {
            var collection = GetCollection();

            await collection.ReplaceOneAsync(x => x.Id.Equals(entity.Id), entity, new UpdateOptions
            {
                IsUpsert = true
            });
        }

        private IMongoCollection<TDomainEntity> GetCollection()
        {
            var name = typeof(TDomainEntity).Name;
            return _db.GetCollection<TDomainEntity>(name);
        }
    }
}