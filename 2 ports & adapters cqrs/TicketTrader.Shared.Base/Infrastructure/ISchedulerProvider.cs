using System.Reactive.Concurrency;

namespace TicketTrader.Shared.Base.Infrastructure
{
    public interface ISchedulerProvider
    {
        IScheduler CurrentThread { get; }
        IScheduler Immediate { get; }
        IScheduler NewThread { get; }
        IScheduler ThreadPool { get; }
        IScheduler TaskPool { get; } 
    }
}
