using System.Collections.Generic;
using System.Linq;

namespace Richiban.Linq
{
    public static class WindowedExtensions
    {

        /// <summary>
        /// A sliding window of size 2 over the input sequence
        /// 
        /// Space: O(1), Time: O(n), Evaluation: Lazy
        /// </summary>
        public static IEnumerable<(T, T)> Pairwise<T>(this IEnumerable<T> source)
        {
            using (var e = source.GetEnumerator())
            {
                if (!e.MoveNext()) yield break;

                var current = e.Current;
                var next = e.Current;

                while (e.MoveNext())
                {
                    (current, next) = (next, e.Current);

                    yield return (current, next);
                }
            }
        }

        /// <summary>
        /// A sliding window of size 3 over the input sequence
        /// 
        /// Space: O(1), Time: O(n), Evaluation: Lazy
        /// </summary>
        public static IEnumerable<(T, T, T)> Tripwise<T>(this IEnumerable<T> source)
        {
            using (var e = source.GetEnumerator())
            {
                if (!e.MoveNext()) yield break;

                var current = e.Current;

                if (!e.MoveNext()) yield break;

                var next = e.Current;

                if (!e.MoveNext()) yield break;

                var nextNext = e.Current;

                yield return (current, next, nextNext);

                while (e.MoveNext())
                {
                    (current, next, nextNext) = (next, nextNext, e.Current);

                    yield return (current, next, nextNext);
                }
            }
        }

        /// <summary>
        /// Returns a sliding window of elements taken from the original sequence.
        ///
        /// Space: O(min(n, windowSize)), Time: O(n), Evaluation: Lazy
        /// </summary>
        public static IEnumerable<IReadOnlyList<T>> Windowed<T>(this IEnumerable<T> source, int windowSize)
        {
            var bucket = new List<T>(windowSize);

            using (var e = source.GetEnumerator())
            {
                while (bucket.Count < windowSize)
                {
                    while (bucket.Count < windowSize && e.MoveNext())
                    {
                        bucket.Add(e.Current);
                    }

                    if (bucket.Count == windowSize)
                    {
                        yield return bucket.ToList();
                        bucket.RemoveAt(0);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}
