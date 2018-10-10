using System;
using System.Collections.Generic;

namespace Richiban.Linq
{
    public static class PadExtensions
    {

        /// <summary>
        /// Returns a new sequence that guarantees to contain at least <paramref name="minCount"/>
        /// elements by yielding all elements from the input sequence followed by however many
        /// <paramref name="padElement"/>s are necessary to bring the sequence up to the desired size.
        /// 
        /// Space: O(1), Time: O(n), Evaluation: Lazy
        /// </summary>
        public static IEnumerable<T> Pad<T>(this IEnumerable<T> sequence, int minCount, T padElement)
        {
            if (sequence == null) throw new ArgumentNullException(nameof(sequence));

            var numElementsYielded = 0;
            var e = sequence.GetEnumerator();

            while (e.MoveNext())
            {
                yield return e.Current;
                numElementsYielded++;
            }

            while (numElementsYielded < minCount)
            {
                yield return padElement;
                numElementsYielded++;
            }
        }
    }
}
