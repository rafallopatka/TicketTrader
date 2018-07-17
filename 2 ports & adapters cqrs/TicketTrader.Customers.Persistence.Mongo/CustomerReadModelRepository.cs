using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using TicketTrader.Customers.Domain;
using TicketTrader.Customers.ReadModel;
using TicketTrader.Shared.Persistence.Mongo.ReadSide;

namespace TicketTrader.Customers.Persistence.Mongo
{
    class CustomerReadModelRepository : MongoReadSideRepository<CustomerReadModel>, ICustomerFinder, ICustomerDenormalizer
    {
        public CustomerReadModelRepository(MongoReadSideRepositoryContext context) : base(context)
        {
        }

        public async Task<CustomerReadModel> GetCustomer(string userId)
        {
            var users = await Collection.FindAsync(model => model.UserId == userId);
            return await users.SingleOrDefaultAsync();
        }

        public async Task AddCustomer(Customer customer)
        {
            var entity = new CustomerReadModel
            {
                Id = customer.Id.ToString(),
                UserId = customer.User.Id.ToString(),
                LastName = customer.User.LastName,
                FistName = customer.User.FirstName,
                Email = customer.User.Email,
                PostalCode = customer.User.PostalCode,
                City = customer.User.City,
                Country = customer.User.Country,
                Address = customer.User.Address
            };

            await Collection.InsertOneAsync(entity);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            var entity = new CustomerReadModel
            {
                Id = customer.Id.ToString(),
                UserId = customer.User.Id.ToString(),
                LastName = customer.User.LastName,
                FistName = customer.User.FirstName,
                Email = customer.User.Email,
                PostalCode = customer.User.PostalCode,
                City = customer.User.City,
                Country = customer.User.Country,
                Address = customer.User.Address
            };

            await Collection.ReplaceOneAsync(model => model.Id == entity.Id, entity);
        }
    }
}
