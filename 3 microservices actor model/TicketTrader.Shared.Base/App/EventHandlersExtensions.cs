using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TicketTrader.Shared.Base.CQRS.Commands;
using TicketTrader.Shared.Base.DDD;

namespace TicketTrader.Shared.Base.App
{
    public static class EventHandlersExtensions
    {
        public static App RegisterEventHandlers(this App app, Assembly assembly)
        {
            var eventHandlerType = typeof(IEventHandler<>);
            List<Type> list = assembly.GetAllTypesImplementingOpenGenericInterface(eventHandlerType).ToList();
            List<ImplementationWithContractsMap> contractsWithImplementation = list
                .Select(implementation => implementation.GetAllContractsMatchingOpenGenericInterface(eventHandlerType))
                .ToList();

            contractsWithImplementation.RegisterImplementationsWithContracts(app.Services);

            AppRegistry.EventHandlers[assembly.FullName] = contractsWithImplementation;

            return app;
        }

        public static App.AppHost SubscribeeEventHandlers(this App.AppHost host, Assembly assembly)
        {
            var contractsWithImplementation = AppRegistry.EventHandlers[assembly.FullName];
            IEnumerable<Type> contracts = contractsWithImplementation
                .SelectMany(x => x.Contracts)
                .ToList();

            var eventBus = EventBus.Current;

            foreach (var contract in contracts)
            {
                var handler = host
                    .ServiceProvider
                    .GetService(contract);

                var genericTypeArguments = contract.GenericTypeArguments;
                var typeName = genericTypeArguments.First().FullName;

                MethodInfo method = typeof(EventBus).GetMethod(nameof(EventBus.Subscribe));
                MethodInfo generic = method.MakeGenericMethod(genericTypeArguments);

                var envName = Environment.MachineName;
                generic.Invoke(eventBus, new[] { handler, envName });
            }

            AppRegistry.EventHandlers.Remove(assembly.FullName);

            return host;
        }
    }
}