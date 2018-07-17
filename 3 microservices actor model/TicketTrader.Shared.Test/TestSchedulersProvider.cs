using System.Reactive.Concurrency;
using Microsoft.Reactive.Testing;
using TicketTrader.Shared.Base.Infrastructure;

namespace TicketTrader.Shared.Test
{
    public class TestSchedulersProvider : ISchedulerProvider
    {
        private readonly TestScheduler _currentThread = new TestScheduler();
        private readonly TestScheduler _immediate = new TestScheduler();
        private readonly TestScheduler _newThread = new TestScheduler();
        private readonly TestScheduler _threadPool = new TestScheduler();
        private readonly TestScheduler _taskPool = new TestScheduler();

        IScheduler ISchedulerProvider.CurrentThread => _currentThread;
        IScheduler ISchedulerProvider.Immediate => _immediate;
        IScheduler ISchedulerProvider.NewThread => _newThread;
        IScheduler ISchedulerProvider.ThreadPool => _threadPool;
        IScheduler ISchedulerProvider.TaskPool => _taskPool;

        public TestScheduler CurrentThread => _currentThread;
        public TestScheduler Immediate => _immediate;
        public TestScheduler NewThread => _newThread;
        public TestScheduler ThreadPool => _threadPool;
    }
}
