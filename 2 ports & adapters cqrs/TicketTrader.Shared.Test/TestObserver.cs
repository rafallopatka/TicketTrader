using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reactive.Subjects;

namespace TicketTrader.Shared.Test
{
    public class TestObserver<T> : IObserver<T>
    {
        private readonly IObserver<T> _observerImplementation;
        private readonly ConcurrentBag<T> _receivedValues;

        public Exception ReceivedError { get; private set; }
        public IEnumerable<T> ReceivedValues => _receivedValues;
        public bool IsCompleted { get; private set; }

        public TestObserver()
        {
            var subject = new Subject<T>();
            _observerImplementation = subject;
            _receivedValues = new ConcurrentBag<T>();
        }

        public void OnCompleted()
        {
            IsCompleted = true;
            _observerImplementation.OnCompleted();
        }

        public void OnError(Exception error)
        {
            _observerImplementation.OnError(error);
            ReceivedError = error;
        }

        public void OnNext(T value)
        {
            _receivedValues.Add(value);
            _observerImplementation.OnNext(value);
        }
    }
}
