using System.Collections.Generic;
using System.Linq;

namespace Aperea
{
    public static class EnumerableExtensions
    {
        public static T[] ToArraySafe<T>(this IEnumerable<T> enumerable)
        {
            return enumerable as T[] ?? enumerable.ToArray();
        }
    }
}