using System;
using System.Collections.Generic;

namespace TicketTrader.Shared.Base.App
{
    internal class ImplementationWithContractsMap
    {
        public Type Implementation { get; set; }
        public IEnumerable<Type> Contracts { get; set; }
    }
}