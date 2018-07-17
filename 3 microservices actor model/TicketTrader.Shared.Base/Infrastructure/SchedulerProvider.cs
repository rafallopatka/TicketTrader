using System.Reactive.Concurrency;

namespace TicketTrader.Shared.Base.Infrastructure
{
    public class SchedulerProvider : ISchedulerProvider
    {
        public IScheduler CurrentThread => CurrentThreadScheduler.Instance;

        public IScheduler Immediate => ImmediateScheduler.Instance;

        public IScheduler NewThread => NewThreadScheduler.Default;

        public IScheduler ThreadPool => ThreadPoolScheduler.Instance;

        public IScheduler TaskPool => TaskPoolScheduler.Default;
    }
}
