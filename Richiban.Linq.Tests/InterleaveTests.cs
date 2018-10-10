using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Richiban.Linq.Tests
{
    public class InterleaveTests
    {
        [Fact]
        void Interleave()
        {

            AssertEqual("Interleave",
                new object[] { 1, 2, 3 }.Interleave(new object[] { 'a', 'b', 'c' }),
                new object[] { 1, 'a', 2, 'b', 3, 'c' });
        }
        [Fact]
        void Interleave2()
        {

            AssertEqual("Interleave",
                new[] { 1, 2, 3 }.Interleave(new[] { 4, 5, 6 }),
                new[] { 1, 4, 2, 5, 3, 6 });
        }
        [Fact]
        void Interleave3()
        {

            AssertEqual("Interleave",
                new[] { 1, 2, 3, 4 }.Interleave(new[] { 7, 8, 9 }),
                new[] { 1, 7, 2, 8, 3, 9, 4 });
        }
        [Fact]
        void Uninterleave()
        {

            AssertEqual2("Uninterleave",
                new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Uninterleave(3),
                new[] { new[] { 1, 4, 7 }, new[] { 2, 5, 8 }, new[] { 3, 6, 9 } });
        }


        void AssertEqual<T>(string topic, IEnumerable<T> actual, IEnumerable<T> expected)
        {
            var result = Enumerable.SequenceEqual(actual, expected);

            if (!result)
            {
                throw new Exception(topic);
            }
        }

        void AssertEqual2<T>(
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