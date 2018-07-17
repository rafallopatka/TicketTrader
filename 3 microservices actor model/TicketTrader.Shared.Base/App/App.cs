using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace TicketTrader.Shared.Base.App
{
    public class App
    {
        public static App Current = new App();

        private App()
        {
            Services = new ServiceCollection();
        }

        public ServiceCollection Services { get; }

        public AppHost Build()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(Services);
            var container =  containerBuilder.Build();
            IServiceProvider serviceProvider = new AutofacServiceProvider(container);
            AppServiceProvider.Current = serviceProvider;

            return new AppHost(serviceProvider);
        }

        public class AppHost
        {
            public IServiceProvider ServiceProvider { get; }

            internal AppHost(IServiceProvider serviceProvider)
            {
                ServiceProvider = serviceProvider;
            }

            public void RunBlocking()
            {
                Console.ReadLine();
            }

            public void RunSilently()
            {
            }
        }
    }
}
