using System;
using System.Collections.Generic;

namespace Richiban.Linq
{
    public static class PartitionExtensions
    {

        /// <summary>
        /// Feeds every element of a sequence into one of two lists, depending on the result of
        /// the predicate on that element.
        ///
        /// Space: O(n), Time: O(n), Evaluation: Eager
        /// </summary>
        public static (List<T> trueValues, List<T> falseValues) Partition<T>(
            this IEnumerable<T> source, Func<T, bool> predicate)
        {
            var trueValues = new List<T>();
            var falseValues = new List<T>();

            foreach (var item in source)
            {
                (predicate(item) ? trueValues : falseValues).Add(item);
            }

            return (trueValues, falseValues);
        }
    }
}
