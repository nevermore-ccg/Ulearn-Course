﻿using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Autocomplete;

public class RightBorderTask
{
    public static int GetRightBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
    {
        while (right - left > 1)
        {
            var middle = (right + left) / 2;
            if (string.Compare(prefix, phrases[middle], StringComparison.InvariantCultureIgnoreCase) >= 0
               || phrases[middle].StartsWith(prefix))
                left = middle;
            else right = middle;
        }
        return right;
    }
}