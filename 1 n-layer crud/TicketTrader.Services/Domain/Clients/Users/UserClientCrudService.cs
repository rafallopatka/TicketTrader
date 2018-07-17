using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketTrader.Dal;
using TicketTrader.Model;
using TicketTrader.Services.Mappings;

namespace TicketTrader.Services.Domain.Clients.Users
{
    public class UserClientCrudService : IUserClientCrudService
    {
        private readonly DalContext _context;

        public UserClientCrudService(DalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserClientDto>> GetUserClientAsync(string identityUserId)
        {
            var data = await _context.IndividualClients
                .Where(x => x.IdentityUserId == identityUserId)
                .ToListAsync();

            return data.MapTo<IEnumerable<UserClientDto>>();
        }

        public async Task<UserClientDto> CreateUserClientAsync(UserDto user)
        {
            var entity = user.MapTo<IndividualClient>();
            var newEntity = _context.IndividualClients.Add(entity);
            await _context.SaveChangesAsync();
            return newEntity.Entity.MapTo<UserClientDto>();
        }
    }
}