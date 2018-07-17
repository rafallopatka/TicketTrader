using System.Threading.Tasks;

namespace TicketTrader.Shared.Base.DDD
{
    public abstract class Repository<TEntity> where TEntity : DomainEntity
    {
        public abstract Task<TEntity> Get(Id id);
        public abstract Task Save(TEntity entity);

        public async Task Delete(Id id)
        {
            var entity = await Get(id);
            entity.MarkAsArchived();
            await Save(entity);
        }

        public async void Delete(TEntity entity)
        {
            entity.MarkAsArchived();
            await Save(entity);
        }
    }
}