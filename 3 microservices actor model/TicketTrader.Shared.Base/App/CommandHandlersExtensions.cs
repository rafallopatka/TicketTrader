using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TicketTrader.Shared.Base.CQRS.Commands;

namespace TicketTrader.Shared.Base.App
{
    public static class CommandHandlersExtensions
    {
        public static App RegisterCommandHandlers(this App app, Assembly assembly)
        {
            var commandHandlerType = typeof(ICommandHandler<>);
            List<Type> list = assembly.GetAllTypesImplementingOpenGenericInterface(commandHandlerType).ToList();
            List<ImplementationWithContractsMap> contractsWithImplementation = list
                .Select(implementation => implementation.GetAllContractsMatchingOpenGenericInterface(commandHandlerType))
                .ToList();

            contractsWithImplementation.RegisterImplementationsWithContracts(app.Services);

            AppRegistry.CommandHandlers[assembly.FullName] = contractsWithImplementation;

            return app;
        }

        public static App.AppHost SubscribeCommandHandlers(this App.AppHost host, Assembly assembly)
        {
            var contractsWithImplementation = AppRegistry.CommandHandlers[assembly.FullName];
            IEnumerable<Type> contracts = contractsWithImplementation
                .SelectMany(x => x.Contracts)
                .ToList();

            var commandBus = CommandBus.Current;

            foreach (var contract in contracts)
            {
                var handler = host
                    .ServiceProvider
                    .GetService(contract);

                MethodInfo method = typeof(CommandBus).GetMethod(nameof(CommandBus.Subscribe));
                MethodInfo generic = method.MakeGenericMethod(contract.GenericTypeArguments);
                generic.Invoke(commandBus, new[] { handler, null });
            }

            AppRegistry.CommandHandlers.Remove(assembly.FullName);

            return host;
        }
    }
}

