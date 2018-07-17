using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TicketTrader.Shared.Base.CQRS.Queries;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Shared.Base.App
{
    public static class BaseTypesRegustratuibExtensions
    {
        public static App RegisterFactories(this App app, Assembly assembly)
        {
            var type = typeof(AggregateFactory);
            var factories = assembly.GetAllTypesDerriviedFrom(type).ToList();

            foreach (var factory in factories)
            {
                app.Services.AddTransient(factory);
            }

            return app;
        }

        public static App RegisterServices(this App app, Assembly assembly)
        {
            var type = typeof(IService);
            List<Type> services = assembly.GetAllTypesImplementingClosedInterface(type).ToList();

            var contracts = services
                .Select(x => x.GetAllContractsMatchingInterface(type))
                .ToList();

            contracts.RegisterImplementationsWithContracts(app.Services);

            return app;
        }

        public static App RegisterFinders(this App app, Assembly assembly)
        {
            var type = typeof(IFinder);
            List<Type> services = assembly.GetAllTypesImplementingClosedInterface(type).ToList();

            var contracts = services
                .Select(x => x.GetAllContractsMatchingInterface(type))
                .ToList();

            contracts.RegisterImplementationsWithContracts(app.Services);

            return app;
        }

        public static App RegisterDenormalizers(this App app, Assembly assembly)
        {
            var type = typeof(IDenormalizer);
            List<Type> services = assembly.GetAllTypesImplementingClosedInterface(type).ToList();

            var contracts = services
                .Select(x => x.GetAllContractsMatchingInterface(type))
                .ToList();

            contracts.RegisterImplementationsWithContracts(app.Services);

            return app;
        }
    }
}