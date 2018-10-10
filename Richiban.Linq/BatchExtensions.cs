using System;
using System.Collections.Generic;

namespace Richiban.Linq
{
    public static class BatchExtensions
    {

        /// <summary>
        /// Batches a sequence up into a sequence of subsequences. All batches of elements will be the 
        /// same size except the last, which can be any size 'n': 1 &lt;= n &lt;= batchSize.
        /// 
        /// Space: O(1), Time: O(n), Evaluation: Lazy
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> sequence, int batchSize)
        {
            if (sequence == null) throw new ArgumentNullException(nameof(sequence));
            if (batchSize <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(batchSize),
                    $"The batch size must be an integer greater than zero");

            var e = sequence.GetEnumerator();

            while (e.MoveNext())
            {
                yield return GenerateBatch();
            }

            IEnumerable<T> GenerateBatch()
            {
                var currentBatchCount = 1;

                yield return e.Current;

                while (currentBatchCount < batchSize && e.MoveNext())
                {
                    yield return e.Current;
                    currentBatchCount++;
                }
            }
        }

        /// <summary>
        /// Batches a sequence into subsequences, breaking one sequence and starting another whenever the
        /// result of the <paramref name="batchBy"/> changes.
        /// 
        /// Space: O(1), Time: O(n), Evaluation: Lazy
        /// </summary>
        public static IEnumerable<IEnumerable<T>> BatchBy<T, BatchKey>(this IEnumerable<T> sequence, Func<T, BatchKey> batchBy)
        {
            if (sequence == null) throw new ArgumentNullException(nameof(sequence));
            if (batchBy == null) throw new ArgumentNullException(nameof(batchBy));

            using (var e = sequence.GetEnumerator())
            {
                var elementWaiting = false;

                while (elementWaiting || e.MoveNext())
                {
                    yield return GenerateBatch();
                }

                IEnumerable<T> GenerateBatch()
                {
                    var batchKey = batchBy(e.Current);

                    yield return e.Current;

                    while (e.MoveNext())
                    {
                        if (batchBy(e.Current).Equals(batchKey) == false)
                        {
                            elementWaiting = true;
                            yield break;
                        }

                        yield return e.Current;
                    }

                    elementWaiting = false;
                }
            }
        }
    }
}
