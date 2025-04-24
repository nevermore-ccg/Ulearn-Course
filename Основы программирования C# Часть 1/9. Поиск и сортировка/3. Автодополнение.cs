using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Autocomplete;

internal class AutocompleteTask
{
    public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
    {
        var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
        if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
            return phrases[index];
        return null;
    }

    public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
    {
        int totalCount = Math.Min(GetCountByPrefix(phrases, prefix), count);
        var startIndex = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
        string[] result = new string[totalCount];
        for (int i = 0; i < totalCount; i++)
            result[i] = phrases[startIndex + i];
        return result;
    }

    public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
    {
        int left = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
        int right = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count);
        return right - left - 1;
    }
}

[TestFixture]
public class AutocompleteTests
{
    [Test]
    public void TopByPrefix_IsEmpty_WhenNoPhrases()
    {
        var phrases = new List<string>() { "a", "ab", "abc", "b", "ba", "bac" };
        string prefix = "c";
        int count = 3;
        var expectedResult = new string[0];

        var actualResult = AutocompleteTask.GetTopByPrefix(phrases, prefix, count);

        Assert.AreEqual(expectedResult, actualResult);
    }

    [Test]
    public void CountByPrefix_IsTotalCount_WhenEmptyPrefix()
    {
        var phrases = new List<string> { "a", "ab", "abc", "b", "ba", "bac" };
        string prefix = "";
        int expectedCount = phrases.Count;

        var actualCount = AutocompleteTask.GetCountByPrefix(phrases, prefix);

        Assert.AreEqual(expectedCount, actualCount);
    }

    [Test]
    public void TopByPrefix_IsLesserCount_WhenCountHuge()
    {
        var phrases = new List<string> { "a", "ab", "abc", "b", "ba", "bac" };
        string prefix = "a";
        int count = 1000000000;
        var expectedResult = new string[] { "a", "ab", "abc" };

        var actualResult = AutocompleteTask.GetTopByPrefix(phrases, prefix, count);

        Assert.AreEqual(expectedResult, actualResult);
    }
}