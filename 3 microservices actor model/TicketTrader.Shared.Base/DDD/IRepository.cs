using System.Threading.Tasks;

namespace TicketTrader.Shared.Base.DDD
{
    public interface IRepository<TEntity> where TEntity : DomainEntity
    {
        Task<TEntity> Get(Id id);
        Task Save(TEntity entity);
        Task Delete(Id id);
        void Delete(TEntity entity);
    }
}
