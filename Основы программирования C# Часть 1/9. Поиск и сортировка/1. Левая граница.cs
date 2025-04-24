using System;
using System.Collections.Generic;
using System.Linq;

namespace Autocomplete;

public class LeftBorderTask
{
    public static int GetLeftBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
    {
        if (right - left == 1) return left;
        var middle = (left + right) / 2;
        if (string.Compare(prefix, phrases[middle], StringComparison.InvariantCultureIgnoreCase) <= 0)
            return GetLeftBorderIndex(phrases, prefix, left, middle);
        return GetLeftBorderIndex(phrases, prefix, middle, right);
    }
}