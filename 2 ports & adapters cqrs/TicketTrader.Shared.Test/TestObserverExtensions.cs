using System;
using System.Reactive.Linq;
using Microsoft.Reactive.Testing;

namespace TicketTrader.Shared.Test
{
    public static class TestObserverExtensions
    {
        public static TestObserver<T> CreateTestObserver<T>(this IObservable<T> observable, TestScheduler testScheduler)
        {
            var observer = new TestObserver<T>();
            observable
                .SubscribeOn(testScheduler)
                .ObserveOn(testScheduler)
                .Subscribe(observer);

            testScheduler.Start();
            return observer;
        }
    }
}
