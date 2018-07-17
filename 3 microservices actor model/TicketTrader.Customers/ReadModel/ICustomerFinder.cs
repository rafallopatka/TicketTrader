using System.Threading.Tasks;
using TicketTrader.Shared.Base.CQRS.Queries;

namespace TicketTrader.Customers.ReadModel
{
    public interface ICustomerFinder: IFinder
    {
        Task<CustomerReadModel> GetCustomer(string userId);
    }
}
