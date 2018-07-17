using System;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.DependencyInjection;

namespace TicketTrader.Management
{
    class Scheduler
    {
        private readonly IServiceProvider _provider;

        public Scheduler(IServiceProvider provider)
        {
            _provider = provider;
        }

        public void Begin()
        {
            GlobalConfiguration.Configuration.UseMemoryStorage();

            try
            {
                RecurringJob.AddOrUpdate(() => DiscardReservations(), Cron.MinuteInterval(1));
                RecurringJob.AddOrUpdate(() => PayOrders(), Cron.MinuteInterval(3));
                RecurringJob.AddOrUpdate(() => DeliverOrders(), Cron.MinuteInterval(5));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void DeliverOrders()
        {
            var service = _provider.GetService<DeliveryJob>();
            service.DeliiverAsync().GetAwaiter();
        }

        public void PayOrders()
        {
            var service = _provider.GetService<PayJob>();
            service.FinalizeAsync().GetAwaiter();
        }

        public void DiscardReservations()
        {
            var service = _provider.GetService<DiscardJob>();
            service.DiscardReservationsAsync().GetAwaiter();
        }
    }
}