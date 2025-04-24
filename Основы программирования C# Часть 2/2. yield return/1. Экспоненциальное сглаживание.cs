using System.Collections.Generic;

namespace yield;

public static class ExpSmoothingTask
{
    public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
    {
        DataPoint previousPoint = null;
        foreach (var point in data)
        {
            if (previousPoint is null)
            {
                previousPoint = point.WithExpSmoothedY(point.OriginalY);
                yield return previousPoint;
            }
            else
            {
                var expSmoothedY = previousPoint.ExpSmoothedY + alpha * (point.OriginalY - previousPoint.ExpSmoothedY);
                previousPoint = point.WithExpSmoothedY(expSmoothedY);
                yield return previousPoint;
            }
        }
    }
}