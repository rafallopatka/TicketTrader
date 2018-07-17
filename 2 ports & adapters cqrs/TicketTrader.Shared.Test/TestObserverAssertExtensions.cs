using System;
using System.Collections.Generic;
using FluentAssertions;

namespace TicketTrader.Shared.Test
{
    public static class TestObserverAssertExtensions
    {
        public static void AssertCompleted<T>(this TestObserver<T> observer)
        {
            observer.IsCompleted.Should().BeTrue("Observable should be in completed state");
        }

        public static void AssertNoErrors<T>(this TestObserver<T> observer)
        {
            observer.ReceivedError.Should().BeNull("Observable should not raise any exception");
        }

        public static void AssertValuesCount<T>(this TestObserver<T> observer, int expectedCount)
        {
            observer.ReceivedValues.Should().HaveCount(expectedCount, $"Observable should returns {expectedCount} values");
        }

        public static void AssertPredicate<T>(this TestObserver<T> observer, Func<IEnumerable<T>, bool> preficate)
        {
            preficate.Invoke(observer.ReceivedValues).Should().BeTrue("Observable should match predicate");
        }
    }
}
