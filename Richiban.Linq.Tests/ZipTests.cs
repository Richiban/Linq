using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Richiban.Linq.Tests
{
    public class ZipTests : TestsBase
    {
        [Fact]
        void EmptyInputReturnsEmptyOutput()
        {
            Assert.Equal(new (string, char)[0], new string[0].Zip(new char[0]));
        }

        [Fact]
        void NullInputThrowsException()
        {
            var input = (IEnumerable<string>)null;

            Assert.Throws<ArgumentNullException>(() => input.Zip(new string[0]).First());
        }

        [Fact]
        void NullInputThrowsException2()
        {
            Assert.Throws<ArgumentNullException>(() => (new string[0]).Zip<string, string>(null).First());
        }

        [Fact]
        void ZippingTwoSequencesOfUnequalLengthYieldsSequenceWithLengthOfShorterInput()
        {
            var (shorterLength, longerLength) = Sort(Random.Next(10), Random.Next(10));

            var shorterSequence = Enumerable.Range(0, shorterLength);
            var longerSequence = Enumerable.Range(0, longerLength);

            var result = shorterSequence.Zip(longerSequence);

            Assert.Equal(shorterLength, result.Count());
        }

        [Fact]
        void SkipWhileZip()
        {
            AssertEqual("SkipWhileZip",
                new[] { -2, -1, 0, 1, 2, 3, 4, -1 }.SkipWhileZip(new[] { 'a', 'b', 'c', 'd', 'e' },
                    (x, y) => x <= 0 ? (true, false) : (false, false)),
                new[] { (1, 'a'), (2, 'b'), (3, 'c'), (4, 'd'), (-1, 'e') });
        }

        public (T, T) Sort<T>(T x, T y) where T : IComparable<T>
        {
            if (x.CompareTo(y) < 0) return (x, y);
            else return (y, x);
        }
    }
}