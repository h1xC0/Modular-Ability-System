using System;
using System.Collections.Generic;
using System.Linq;

namespace Shared.Extensions
{
    public static class LinqExt
    {
        public static IEnumerable<T> Each<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null)
                throw new NullReferenceException();

            if (action == null)
                throw new NullReferenceException("No action");

            var forEach = source.ToList();
            foreach (T element in forEach)
            {
                action?.Invoke(element);
            }

            source = forEach;

            return source;
        }
    }
}