using System;
using System.Collections.Generic;
using System.Linq;

namespace Richiban.Linq.Tests
{
    public abstract class TestsBase
    {
        protected readonly static Random Random = new Random();

        protected void AssertEqual<T>(string topic, IEnumerable<T> actual, IEnumerable<T> expected)
        {
            var result = Enumerable.SequenceEqual(actual, expected);

            if (!result)
            {
                throw new Exception(topic);
            }
        }

        protected void AssertEqual<T>(
            string topic,
            (IEnumerable<T>, IEnumerable<T>) actual,
            (IEnumerable<T>, IEnumerable<T>) expected)
        {
            var result =
                Enumerable.SequenceEqual(actual.Item1, expected.Item1)
                && Enumerable.SequenceEqual(actual.Item2, expected.Item2);

            if (!result)
            {
                throw new Exception(topic);
            }
        }

        protected void AssertEqual2<T>(
            string topic,
            IEnumerable<IEnumerable<T>> actual,
            IEnumerable<IEnumerable<T>> expected)
        {
            var result = true;

            foreach (var (a, e) in actual.Zip(expected, (x, y) => (x, y)))
            {
                foreach (var (a1, e1) in a.Zip(e, (x, y) => (x, y)))
                {
                    if (a1.Equals(e1) == false)
                    {
                        result = false;
                        goto end;
                    }
                }
            }

            end:

            if (!result)
            {
                throw new Exception(topic);
            }
        }
    }
}