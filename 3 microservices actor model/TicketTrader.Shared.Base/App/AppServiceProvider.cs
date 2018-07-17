using System;
using Microsoft.Extensions.DependencyInjection;

namespace TicketTrader.Shared.Base.App
{
    public static class AppServiceProvider
    {
        public static IServiceProvider Current { get; set; }

        public static TService Resolve<TService>()
        {
            return Current.GetService<TService>();
        }
    }
}
