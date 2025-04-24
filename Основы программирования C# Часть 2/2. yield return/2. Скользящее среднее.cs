using System.Collections;
using System.Collections.Generic;

namespace yield;

public static class MovingAverageTask
{
    public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
    {
        double windowSum = 0;
        var windowQueue = new Queue<double>();
        foreach (var point in data)
        {
            windowQueue.Enqueue(point.OriginalY);
            windowSum += point.OriginalY;
            if (windowQueue.Count > windowWidth)
                windowSum -= windowQueue.Dequeue();
            yield return point.WithAvgSmoothedY(windowSum / windowQueue.Count);
        }
    }
}