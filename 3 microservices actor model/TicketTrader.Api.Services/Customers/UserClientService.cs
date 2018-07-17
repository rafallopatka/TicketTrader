using System;
using System.Threading.Tasks;
using TicketTrader.Customers.Canonical.Commands;
using TicketTrader.Customers.Canonical.Queries;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Api.Services.Customers
{
    public class UserClientService
    {
        public async Task<UserClientDto> GetClientForUserAsync(string userId)
        {
            var query = new GetCustomerForUserQuery
            {
                UserId = userId
            };

            var response = await QueryBus.Current.Query<GetCustomerForUserQuery, GetCustomerForUserQuery.Response>(query);
            var value = response.Value;

            if (value == null)
            {
                return null;
            }

            return new UserClientDto
            {
                ClientId = value.CustomerId,
                IdentityUserId = value.UserId,
                FistName = value.FistName,
                LastName = value.LastName,
                Email = value.Email,
                Address = value.Address,
                City = value.City,
                Country = value.Country,
                PostalCode = value.PostalCode
            };
        }

        public async Task<UserClientDto> CreateClientForUserAsync(UserDto user)
        {
            var command = new CreateCustomerForUserCommand
            {
                UserId = user.IdentityUserId,
                CustomerId = Guid.NewGuid().ToString(),
                Address = user.Address,
                City = user.City,
                Country = user.Country,
                Email = user.Email,
                FirstName = user.FistName,
                LastName = user.LastName,
                PostalCode = user.PostalCode,
            };

            await CommandBus.Current.DispatchAsync(command);

            return new UserClientDto
            {
                IdentityUserId = user.IdentityUserId,
                ClientId = user.IdentityUserId,
                Address = user.Address,
                City = user.City,
                Country = user.Country,
                Email = user.Email,
                FistName = user.FistName,
                LastName = user.LastName,
                PostalCode = user.PostalCode,
            };
        }
    }
}