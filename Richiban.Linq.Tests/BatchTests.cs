using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Richiban.Linq.Tests
{
    public class BatchTests : TestsBase
    {
        [Fact]
        void NullInputThrowsException()
        {
            var input = (IEnumerable<string>)null;

            Assert.Throws<ArgumentNullException>(() => input.Batch(100).First());
        }

        [Fact]
        void BatchSizeZeroOrLessThrowsException()
        {
            var input = Enumerable.Range(0, Random.Next());
            var batchSize = 0 - Random.Next();

            Assert.Throws<ArgumentOutOfRangeException>(() => input.Batch(batchSize).First());
        }

        [Fact]
        void FinalBatchHasCountLessThanOrEqualToBatchSize()
        {
            var inputCount = Random.Next(2, 1000);
            var input = Enumerable.Range(0, inputCount);
            var batchSize = Random.Next(inputCount - 1);

            var results = input.Batch(batchSize);
            var finalBatch = results.Last();
            var finalBatchCount = finalBatch.Count();

            Assert.True(finalBatchCount <= batchSize);
        }

        [Fact]
        void NonFinalBatchesHaveCountEqualToBatchSize()
        {
            var inputCount = Random.Next(2, 1000);
            var input = Enumerable.Range(0, inputCount);
            var batchSize = Random.Next(inputCount - 1);

            var results = input.Batch(batchSize).Select(x => x.ToList()).ToList();
            results.RemoveAt(results.Count - 1);

            var batchNum = 0;
            foreach (var batch in results)
            {
                Assert.Equal(batchSize, batch.Count());
                batchNum++;
            }
        }

        [Fact]
        void Batch()
        {
            AssertEqual2("Batch",
                new[] { 1, 2, 3, 4, 5, 6, 7, 8 }.Batch(3).Select(x => x.ToArray()),
                new[] { new[] { 1, 2, 3 }, new[] { 4, 5, 6 }, new[] { 7, 8 } });
        }

        [Fact]
        void BatchBy()
        {

            AssertEqual2("BatchBy",
                new[] { 1, 3, 5, 2, 4, 6, 5, 7, 8, 6 }.BatchBy(x => x % 2),
                new[] { new[] { 1, 3, 5 }, new[] { 2, 4, 6 }, new[] { 5, 7 }, new[] { 8, 6 } });
        }
    }
}