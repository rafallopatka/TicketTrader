using System;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Customers.Domain
{
    public class CustomerFactory : AggregateFactory
    {
        public Customer Create(Id id, User user)
        {
            if (id == null)
                throw new InvalidOperationException("Id cannot be null");

            if (user == null)
            {
                throw new InvalidOperationException("User cannot be null");
            }

            var customer = new Customer(id, user);

            return customer;
        }
    }
}