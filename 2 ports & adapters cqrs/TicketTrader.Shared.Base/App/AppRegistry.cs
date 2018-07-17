using System;
using System.Collections.Generic;

namespace TicketTrader.Shared.Base.App
{
    internal static class AppRegistry
    {
        public static Dictionary<string, List<ImplementationWithContractsMap>> CommandHandlers { get; }
        public static Dictionary<string, List<ImplementationWithContractsMap>> EventHandlers { get; set; }
        public static Dictionary<string, List<ImplementationWithContractsMap>> QueryHandlers { get; set; }

        static AppRegistry()
        {
            CommandHandlers = new Dictionary<string, List<ImplementationWithContractsMap>>();
            EventHandlers = new Dictionary<string, List<ImplementationWithContractsMap>>();
            QueryHandlers = new Dictionary<string, List<ImplementationWithContractsMap>>();
        }
    }
}