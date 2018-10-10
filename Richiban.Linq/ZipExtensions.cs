using System;
using System.Collections.Generic;

namespace Richiban.Linq
{
    public static class ZipExtensions
    {
        /// <summary>
        /// Enumerates two sequences at the same time, yielding pairs of elements (one from each sequence).
        /// 
        /// The enumeration ends as soon as one of the source sequences runs out of elements.
        /// 
        /// Space: O(1), Time: O(min(n, m)), Evaluation: Lazy
        /// </summary>
        public static IEnumerable<(T1, T2)> Zip<T1, T2>(
            this IEnumerable<T1> leftSequence, IEnumerable<T2> rightSequence)
        {
            if (leftSequence == null)
            {
                throw new ArgumentNullException(nameof(leftSequence));
            }

            if (rightSequence == null)
            {
                throw new ArgumentNullException(nameof(rightSequence));
            }

            var (e1, e2) = (leftSequence.GetEnumerator(), rightSequence.GetEnumerator());

            while (e1.MoveNext() && e2.MoveNext())
            {
                yield return (e1.Current, e2.Current);
            }
        }

        /// <summary>
        /// Zips two sequences together, with a predicate that can choose to skip elements from 
        /// one or both sequences. As soon as the predicate returns (false, false), i.e. that neither 
        /// element should be skipped, all remaining elements are yielded.
        /// 
        /// Space: O(1), Time: O(min(n, m)), Evaluation: Lazy
        /// </summary>
        public static IEnumerable<(L, R)> SkipWhileZip<L, R>(
            this IEnumerable<L> leftSource, IEnumerable<R> rightSource,
            Func<L, R, (bool skipLeftItem, bool skipRightItem)> skipPredicate)
        {
            var eL = leftSource.GetEnumerator();
            var eR = rightSource.GetEnumerator();

            if (eL.MoveNext() == false || eR.MoveNext() == false) yield break;

            while (true)
            {
                var (skipLeft, skipRight) = skipPredicate(eL.Current, eR.Current);

                if (skipLeft == false && skipRight == false) break;

                if (skipLeft)
                {
                    if (eL.MoveNext()) { }
                    else yield break;
                }

                if (skipRight)
                {
                    if (eR.MoveNext()) { }
                    else yield break;
                }
            }

            do
            {
                yield return (eL.Current, eR.Current);
            }
            while (eL.MoveNext() && eR.MoveNext());
        }
    }
}
