using System.Threading.Tasks;
using TicketTrader.Customers.Domain;
using TicketTrader.Shared.Base.CQRS.Queries;

namespace TicketTrader.Customers.ReadModel
{
    public interface ICustomerDenormalizer: IDenormalizer
    {
        Task AddCustomer(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
    }
}
