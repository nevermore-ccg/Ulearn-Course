using System;
using System.Collections.Generic;

// Каждый документ — это список токенов. То есть List<string>.
// Вместо этого будем использовать псевдоним DocumentTokens.
// Это поможет избежать сложных конструкций:
// вместо List<List<string>> будет List<DocumentTokens>
using DocumentTokens = System.Collections.Generic.List<string>;

namespace Antiplagiarism;

public class LevenshteinCalculator
{
    public List<ComparisonResult> CompareDocumentsPairwise(List<DocumentTokens> documents)
    {
        var result = new List<ComparisonResult>();
        for (var i = 0; i < documents.Count; i++)
            for (var j = i + 1; j < documents.Count; j++)
            {
                var distance = CalculateLevensteinDistance(documents[i], documents[j]);
                result.Add(new ComparisonResult(documents[i], documents[j], distance));
            }
        return result;
    }

    private double CalculateLevensteinDistance(DocumentTokens first, DocumentTokens second)
    {
        var opt = new double[first.Count + 1, second.Count + 1];
        for (var i = 0; i <= first.Count; i++)
            opt[i, 0] = i;
        for (var i = 0; i <= second.Count; i++)
            opt[0, i] = i;
        for (var i = 1; i <= first.Count; i++)
            for (var j = 1; j <= second.Count; j++)
            {
                if (first[i - 1] == second[j - 1])
                    opt[i, j] = opt[i - 1, j - 1];
                else
                    opt[i, j] = Math.Min(opt[i - 1, j - 1] +
                        TokenDistanceCalculator.GetTokenDistance(first[i - 1], second[j - 1]),
                        1 + Math.Min(opt[i - 1, j], opt[i, j - 1]));
            }
        return opt[first.Count, second.Count];
    }
}