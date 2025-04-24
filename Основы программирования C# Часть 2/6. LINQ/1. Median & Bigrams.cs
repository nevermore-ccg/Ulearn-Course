using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews;

public static class ExtensionsTask
{
    public static double Median(this IEnumerable<double> items)
    {
        var list = items.OrderBy(x => x).ToList();
        if (list.Count() == 0) throw new InvalidOperationException();
        if (list.Count() % 2 == 0)
            return (list[list.Count() / 2] + list[list.Count() / 2 - 1]) / 2;
        else
            return list[list.Count() / 2];
    }

    public static IEnumerable<(T First, T Second)> Bigrams<T>(this IEnumerable<T> items)
    {
        bool isFirstEntry = true;
        T first = default;
        T second = default;
        foreach (var item in items)
        {
            if (isFirstEntry)
            {
                first = item;
                isFirstEntry = false;
            }
            else
            {
                second = item;
                yield return (first, second);
                first = second;
            }
        }
    }
}