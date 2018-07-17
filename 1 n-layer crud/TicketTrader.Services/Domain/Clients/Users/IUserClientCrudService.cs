using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketTrader.Services.Domain.Clients.Users
{
    public interface IUserClientCrudService
    {
        Task<IEnumerable<UserClientDto>> GetUserClientAsync(string identityUserId);
        Task<UserClientDto> CreateUserClientAsync(UserDto user);
    }
}
