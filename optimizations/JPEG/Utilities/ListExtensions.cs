using System;
using System.Collections.Generic;
using System.Linq;

namespace JPEG.Utilities
{
    static class ListExtensions
    {
        public static T MinOrDefault<T>(this List<T> list, Func<T, int> selector)
        {
            if (list.Count == 0)
                return default;
            var minVal = int.MaxValue;
            var minItem = default(T);
            foreach (var item in list)
            {
                var value = selector(item);
                if (value >= minVal) continue;
                minItem = item;
                minVal = value;
            }

            return minItem;
        }

        public static List<T> Without<T>(this List<T> list, T element)
        {
            var result = new List<T>();
            foreach (var item in list)
            {
                if (!item.Equals(element))
                    result.Add(item);
            }

            return result;
        }
    }
}