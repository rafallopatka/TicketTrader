using System.Reactive.Concurrency;

namespace TicketTrader.Shared.Base.Infrastructure
{
    public static class Schedulers
    {
        public static ISchedulerProvider Provider { private get; set; }

        public static IScheduler CurrentThread => Provider.CurrentThread;
        public static IScheduler Immediate => Provider.Immediate;
        public static IScheduler NewThread => Provider.NewThread;
        public static IScheduler ThreadPool => Provider.ThreadPool;
        public static IScheduler TaskPool => Provider.TaskPool;

        static Schedulers()
        {
            Provider = new SchedulerProvider();
        }
    }
}
