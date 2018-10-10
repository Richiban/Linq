using System;
using System.Collections.Generic;

namespace Richiban.Linq
{
    public static class SplitExtensions
    {

        /// <summary>
        /// Splits a sequence into multiple subsequences in much the same way as String.Split.
        /// 
        /// Note that the <paramref name="separatorElement" /> is not returned in any of the resultant sequences.
        ///
        /// Space: O(1), Time: O(n), Evaluation: Lazy
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Split<T>(
            this IEnumerable<T> source, T separatorElement)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            var e = source.GetEnumerator();

            while (e.MoveNext())
            {
                yield return GenerateBatch();
            }

            IEnumerable<T> GenerateBatch()
            {
                yield return e.Current;

                while (e.MoveNext())
                {
                    if (e.Current.Equals(separatorElement))
                    {
                        yield break;
                    }

                    yield return e.Current;
                }
            }
        }

        /// <summary>
        /// Splits a sequence into multiple subsequences given a predicate that acts on two 
        /// consecutive elements. When the predicate returns true the sequence will be split between
        /// those two elements.
        ///
        /// Space: O(1), Time: O(n), Evaluation: Lazy
        /// </summary>
        public static IEnumerable<IEnumerable<T>> SplitWhen<T>(
            this IEnumerable<T> source, Func<T, T, bool> splitPredicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (splitPredicate == null) throw new ArgumentNullException(nameof(splitPredicate));

            var e = source.GetEnumerator();
            var elementWaiting = false;

            while (elementWaiting || e.MoveNext())
            {
                yield return GenerateBatch();
            }

            IEnumerable<T> GenerateBatch()
            {
                yield return e.Current;

                var prev = e.Current;

                while (e.MoveNext())
                {
                    if (splitPredicate(prev, e.Current))
                    {
                        elementWaiting = true;
                        yield break;
                    }
                    else
                    {
                        yield return e.Current;
                    }
                }

                elementWaiting = false;
            }
        }
    }
}
