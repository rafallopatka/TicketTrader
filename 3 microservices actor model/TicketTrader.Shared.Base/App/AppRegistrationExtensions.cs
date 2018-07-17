using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace TicketTrader.Shared.Base.App
{
    public static class AppRegistrationExtensions
    {
        internal static IEnumerable<Type> GetAllTypesImplementingOpenGenericInterface(this Assembly assembly, Type openGenericType)
        {
            return (from x in assembly.GetTypes()
                where x.IsAbstract == false && x.IsInterface == false
                from z in x.GetInterfaces()
                let y = x.BaseType
                where
                    (y != null && y.IsGenericType &&
                     openGenericType.IsAssignableFrom(y.GetGenericTypeDefinition())) ||
                    (z.IsGenericType &&
                     openGenericType.IsAssignableFrom(z.GetGenericTypeDefinition()))
                select x).Distinct();
        }

        internal static IEnumerable<Type> GetAllTypesImplementingClosedInterface(this Assembly assembly, Type closedType)
        {
            return (from x in assembly.GetTypes()
                where x.IsAbstract == false && x.IsInterface == false
                from z in x.GetInterfaces()
                let y = x.BaseType
                where
                    (closedType.IsAssignableFrom(y)) ||
                    (closedType.IsAssignableFrom(z))
                select x).Distinct();
        }

        internal static IEnumerable<Type> GetAllTypesDerriviedFrom(this Assembly assembly, Type type)
        {
            return from x in assembly.GetTypes()
                where x.BaseType == type
                select x;
        }

        internal static ImplementationWithContractsMap GetAllContractsMatchingOpenGenericInterface(this Type implementation, Type openGenericType)
        {
            List<Type> contracts = implementation
                .GetInterfaces()
                .Where(x => x.IsGenericType)
                .Where(x => openGenericType.GetGenericTypeDefinition() == x.GetGenericTypeDefinition())
                .ToList();

            return new ImplementationWithContractsMap
            {
                Contracts = contracts,
                Implementation = implementation
            };
        }

        internal static ImplementationWithContractsMap GetAllContractsMatchingInterface(this Type implementation, Type closedType)
        {
            List<Type> contracts = implementation
                .GetInterfaces()
                .Where(type => closedType.IsAssignableFrom(type) && closedType != type)
                .ToList();

            return new ImplementationWithContractsMap
            {
                Contracts = contracts,
                Implementation = implementation
            };
        }

        internal static void RegisterImplementationsWithContracts(this IEnumerable<ImplementationWithContractsMap> map, IServiceCollection collection)
        {
            foreach (var service in map)
            {
                foreach (var contract in service.Contracts)
                {
                    collection.AddTransient(contract, service.Implementation);
                }
            }
        }
    }
}