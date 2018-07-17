using System.Threading.Tasks;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Shared.Test
{
    public class TestRepository<TEntity> : IRepository<TEntity> where TEntity : DomainEntity
    {
        public Task<TEntity> Get(Id id)
        {
            throw new System.NotImplementedException();
        }

        public Task Save(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(Id id)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(TEntity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
