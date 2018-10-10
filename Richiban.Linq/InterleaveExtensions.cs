using System;
using System.Collections.Generic;

namespace Richiban.Linq
{
    public static class InterleaveExtensions
    {

        /// <summary>
        /// Alternately enumerates two sequences.
        /// 
        /// Space: O(1), Time: O(n + m), Evaluation: Lazy
        /// </summary>
        public static IEnumerable<object> Interleave(this IEnumerable<object> leftSequence, IEnumerable<object> rightSequence)
        {
            var (currentEnumerator, nextEnumerator) =
                (leftSequence.GetEnumerator(), rightSequence.GetEnumerator());

            while (currentEnumerator.MoveNext())
            {
                yield return currentEnumerator.Current;

                (currentEnumerator, nextEnumerator) = (nextEnumerator, currentEnumerator);
            }
        }

        /// <summary>
        /// Alternately enumerates two sequences
        /// 
        /// Space: O(1), Time: O(n), Evaluation: Lazy
        /// </summary>
        public static IEnumerable<T> Interleave<T>(this IEnumerable<T> leftSequence, IEnumerable<T> rightSequence)
        {
            var (e1, e2) = (leftSequence.GetEnumerator(), rightSequence.GetEnumerator());

            while (e1.MoveNext())
            {
                yield return e1.Current;

                (e1, e2) = (e2, e1);
            }
        }

        /// <summary>
        /// The opposite of interleave: Takes an input sequence and returns
        /// <paramref name="numSequences"> output sequences. All output sequences will have the
        /// same length, to within 1;
        ///
        /// Think of it as dealing cards
        /// 
        /// Space: O(1), Time: O(n), Evaluation: Lazy
        /// </summary>
        public static List<T>[] Uninterleave<T>(
            this IEnumerable<T> source, int numSequences)
        {
            if (source == null) { throw new ArgumentNullException(nameof(source)); }
            if (numSequences <= 0) { throw new ArgumentOutOfRangeException(nameof(numSequences)); }

            var sequences = new List<T>[numSequences];

            if (numSequences == 0) return sequences;

            for (var i = 0; i < numSequences; i++)
            {
                sequences[i] = new List<T>();
            }

            using (var e = source.GetEnumerator())
            {
                var currentSequenceIndex = 0;

                while (e.MoveNext())
                {
                    sequences[currentSequenceIndex].Add(e.Current);
                    currentSequenceIndex = (currentSequenceIndex + 1) % numSequences;
                }
            }

            return sequences;
        }
    }
}
