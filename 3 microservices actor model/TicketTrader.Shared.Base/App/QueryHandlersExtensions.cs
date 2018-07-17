using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Shared.Base.App
{
    public static class QueryHandlersExtensions
    {
        public static App RegisterQueryHandlers(this App app, Assembly assembly)
        {
            var queryHandlerType = typeof(IQueryHandler<,>);
            List<Type> list = assembly.GetAllTypesImplementingOpenGenericInterface(queryHandlerType).ToList();
            List<ImplementationWithContractsMap> contractsWithImplementation = list
                .Select(implementation => implementation.GetAllContractsMatchingOpenGenericInterface(queryHandlerType))
                .ToList();

            contractsWithImplementation.RegisterImplementationsWithContracts(app.Services);

            AppRegistry.QueryHandlers[assembly.FullName] = contractsWithImplementation;

            return app;
        }

        public static App.AppHost SubscribeeQueryHandlers(this App.AppHost host, Assembly assembly)
        {
            var contractsWithImplementation = AppRegistry.QueryHandlers[assembly.FullName];
            IEnumerable<Type> contracts = contractsWithImplementation
                .SelectMany(x => x.Contracts)
                .ToList();

            var queryBus = QueryBus.Current;

            foreach (var contract in contracts)
            {
                var handler = host
                    .ServiceProvider
                    .GetService(contract);

                MethodInfo method = typeof(QueryBus).GetMethod(nameof(QueryBus.Subscribe));
                MethodInfo generic = method.MakeGenericMethod(contract.GenericTypeArguments);
                generic.Invoke(queryBus, new[] {handler});
            }

            AppRegistry.QueryHandlers.Remove(assembly.FullName);

            return host;
        }
    }
}