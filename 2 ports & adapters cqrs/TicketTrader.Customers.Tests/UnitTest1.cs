using System.Reflection;
using TicketTrader.Customers.Domain;
using TicketTrader.Shared.Base.App;
using TicketTrader.Shared.Base.CQRS.Commands;
using TicketTrader.Shared.Base.DDD;
using TicketTrader.Shared.Test;
using Xunit;

namespace TicketTrader.Customers.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            CommandBus.Current = new TestCommandBus();

            var domainAssembly = Assembly.GetAssembly(typeof(Customer));
            var testaAssembly = Assembly.GetAssembly(typeof(UnitTest1));

            var app = App.Current
                .RegisterFactories(domainAssembly)
                .RegisterCommandHandlers(domainAssembly)
                .RegisterQueryHandlers(domainAssembly)
                .RegisterServices(testaAssembly);

            var host = app.Build();

            host
                .SubscribeCommandHandlers(domainAssembly);
        }

    }

    interface IFoo : IService
    {

    }

    class Foo : IFoo
    {

    }
}
